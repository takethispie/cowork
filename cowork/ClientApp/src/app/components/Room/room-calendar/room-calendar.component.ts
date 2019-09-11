import {Component, Input, OnInit} from '@angular/core';
import {CalendarEvent, CalendarEventTimesChangedEvent, CalendarView} from 'angular-calendar';
import {ModalController} from '@ionic/angular';
import {LoadingService} from '../../../services/loading.service';
import {addMinutes, startOfWeek} from 'date-fns';
import {DateTime} from 'luxon';
import {colors} from './colors';
import {Observable, Subject} from 'rxjs';
import {RoomBookingService} from '../../../services/room-booking.service';
import {Room} from '../../../models/Room';
import {map} from 'rxjs/operators';
import {RoomBooking} from '../../../models/RoomBooking';
import {CalendarBooking} from './CalendarBooking';
import {AuthService} from '../../../services/auth.service';
import {ToastService} from '../../../services/toast.service';
import {TimeSlotService} from '../../../services/time-slot.service';

@Component({
    selector: 'app-room-calendar',
    templateUrl: './room-calendar.component.html',
    styleUrls: ['./room-calendar.component.scss'],
})
export class RoomCalendarComponent implements OnInit {
    @Input() room: Room;
    viewDate: Date = new Date();
    events: CalendarEvent[] = [];
    view: CalendarView = CalendarView.Week;
    refresh: Subject<any> = new Subject();

    constructor(private modalCtrl: ModalController, public loading: LoadingService, public toast: ToastService,
                public roomBookingService: RoomBookingService, public auth: AuthService, public timeSlotService: TimeSlotService) { }

    ngOnInit() {
        this.loading.Loading = true;
        this.loadEvents(DateTime.local().minus({months: 3 }));
    }

    loadEvents(date: DateTime) {
        this.roomBookingService.AllOfRoomStartingAtDate(this.room.Id, date).pipe(
            map((data: RoomBooking[]) => data.map(roomBooking => {
                const ret: CalendarBooking & CalendarEvent = CalendarBooking.FromRoomBooking(roomBooking);
                if(roomBooking.ClientId === this.auth.UserId) return this.AddEditionProperties(ret);
                ret.color = colors.blue;
                return ret;
            })),
        ).subscribe({
            next: res => {
                this.events = res;
                this.loadOpeningTimeEvents();
            },
            error: () => {
                this.toast.PresentToast("Erreur lors du chargement des réservations");
                this.loading.Loading = false;
            },
            complete: () => this.loading.Loading = false
        });
    }

    loadOpeningTimeEvents = () => {
        this.timeSlotService.AllFromPlace(this.room.Id).subscribe(res => {
            let loopDay = startOfWeek(this.viewDate, {weekStartsOn: 1});
            console.log(loopDay);
            res.forEach(time => {
                const lock: CalendarEvent = {
                    id: 1,
                    title: "fermé",
                    start: DateTime.local(loopDay.getFullYear(), loopDay.getMonth() + 1, loopDay.getDay(), 7, 0, 0, 0).toJSDate(),
                    end: DateTime.local(loopDay.getFullYear(), loopDay.getMonth() + 1, loopDay.getDay(), time.StartHour, time.StartMinutes, 0, 0).toJSDate(),
                    color: colors.blue,
                    resizable: { beforeStart: false, afterEnd: true },
                    draggable: false
                };
                this.events.push(lock);
                loopDay = DateTime.fromJSDate(loopDay).plus({ day: 1}).toJSDate();
            });
            this.refresh.next();
            console.log(this.events);
        });
    };

    GoBack = () => this.modalCtrl.dismiss(null);


    async HourSegmentClicked(ev: {date: Date}) {
        //we don't want to be able to book past hour segment
        if(ev.date.valueOf() < DateTime.local().valueOf() || ev.date.valueOf() < DateTime.local().valueOf()) return;
        const newEvent = this.CreateBaseCalendarEvent(ev);
        const roomBooking = this.InitRoomBooking(newEvent);
        this.loading.Loading = true;
        this.OverlappingBookingFromOtherRooms(this.auth.UserId, roomBooking).subscribe((otherBookings: CalendarBooking[]) => {
            if (otherBookings.length > 0) {
                this.toast.PresentToast("Vous avez déjà réservé la salle " + otherBookings[0].Room.Name + " pour cet horaire");
                this.loading.Loading = false;
            } else this.roomBookingService.Create(roomBooking).subscribe({
                next: res => {
                    if (res !== -1) {
                        newEvent.id = res;
                        this.events.push(newEvent);
                        this.refresh.next();
                    } else this.toast.PresentToast("Erreur lors de la création de la réservation");
                },
                error: () => {
                    this.toast.PresentToast("Erreur lors de l'ajout de la réservation");
                    this.loading.Loading = false;
                },
                complete: () => this.loading.Loading = false
            });
        });
    }

    private InitRoomBooking(newEvent) {
        const roomBooking = newEvent.ToRoomBooking();
        roomBooking.Client = this.auth.User;
        roomBooking.ClientId = this.auth.UserId;
        roomBooking.Room = this.room;
        roomBooking.RoomId = this.room.Id;
        return roomBooking;
    }

    private CreateBaseCalendarEvent(ev: { date: Date }) {
        let newEvent: CalendarBooking & CalendarEvent = new CalendarBooking();
        newEvent.title = '';
        newEvent.color = colors.yellow;
        newEvent.start = ev.date;
        newEvent.end = addMinutes(ev.date, 30);
        newEvent.draggable = true;
        newEvent.resizable = {
            beforeStart: false,
            afterEnd: true
        };
        newEvent = this.AddEditionProperties(newEvent);
        return newEvent;
    }

    PreviousWeek() {
        const startWeek = startOfWeek(this.viewDate, {weekStartsOn: 1});
        let dateTime = DateTime.fromJSDate(startWeek);
        dateTime = dateTime.minus({ days: 7});
        this.loadEvents(dateTime);
        this.viewDate = dateTime.toJSDate();
    }

    TodayWeek() {
        this.viewDate = startOfWeek(new Date(), {weekStartsOn: 1});
        this.loadEvents(DateTime.fromJSDate(this.viewDate));
    }

    NextWeek() {
        const startWeek = startOfWeek(this.viewDate, {weekStartsOn: 1});
        let dateTime = DateTime.fromJSDate(startWeek);
        dateTime = dateTime.plus({ days: 7});
        this.loadEvents(dateTime);
        this.viewDate = dateTime.toJSDate();
    }

    AddEditionProperties(ret: CalendarBooking & CalendarEvent) {
        //if the event is in the past we don't want to be able to edit it
        if(ret.start.valueOf() < DateTime.local().valueOf() || ret.end.valueOf() < DateTime.local().valueOf()) return ret;
        ret.draggable = true;
        ret.resizable = { beforeStart: false, afterEnd: true };
        ret.actions = [
            {
                label: '<i>Supprimer</i>',
                onClick: ({event}: { event: CalendarEvent & CalendarBooking }) => {
                    this.loading.Loading = true;
                    this.roomBookingService.Delete(event.id).subscribe({
                        next: () => this.ngOnInit(),
                        error: () => {
                            this.toast.PresentToast("Erreur lors de la suppression");
                            this.loading.Loading = false;
                        },
                        complete: () => {
                            this.loading.Loading = false;
                        }
                    });
                }
            }
        ];
        return ret;
    }

    public OverlappingBookingFromOtherRooms(userId: number, roomBooking: RoomBooking): Observable<CalendarBooking[]> {
        const calendarBooking = CalendarBooking.FromRoomBooking(roomBooking);
        return this.roomBookingService.AllOfUser(userId).pipe(
            map((data: RoomBooking[]) => data.map(roomB => CalendarBooking.FromRoomBooking(roomB))),
            map((bookings: CalendarBooking[]) => {
              return bookings
                  .filter(b => b.id !== roomBooking.Id)
                  .filter(booking => !(booking.end.valueOf() <= calendarBooking.start.valueOf()
                      || booking.start.valueOf() >= calendarBooking.end.valueOf()));
            })
        );
    }

    eventTimesChanged({event, newStart, newEnd}: CalendarEventTimesChangedEvent<CalendarBooking>) {
        if (event.start.valueOf() === newStart.valueOf() && event.end.valueOf() === newEnd.valueOf()) return;
        event.start = newStart;
        event.end = newEnd;
        const roomBooking = this.InitRoomBooking(event);
        this.loading.Loading = true;
        //TODO remove sub inside sub
        this.OverlappingBookingFromOtherRooms(this.auth.UserId, roomBooking).subscribe(otherBookings => {
            if (otherBookings.length > 0) {
                this.toast.PresentToast("Vous avez déjà réservé la salle " + otherBookings[0].Room.Name + " pour cet horaire");
                this.loading.Loading = false;
            } else this.roomBookingService.Update(roomBooking).subscribe({
                next: () => this.ngOnInit(),
                error: () => {
                    this.toast.PresentToast("Erreur lors de la modification");
                    this.loading.Loading = false;
                    this.ngOnInit();
                },
                complete: () => this.loading.Loading = false
            });
        });
    }
}
