import {Component, OnInit} from '@angular/core';
import {addMinutes, startOfWeek} from 'date-fns';
import {DateTime} from 'luxon';
import {Observable, Subject} from 'rxjs';
import {map} from 'rxjs/operators';
import {CalendarEvent, CalendarEventTimesChangedEvent, CalendarView} from 'angular-calendar';
import {ModalController} from '@ionic/angular';
import {LoadingService} from '../services/loading.service';
import {ToastService} from '../services/toast.service';
import {AuthService} from '../services/auth.service';
import {WareBookingService} from '../services/ware-booking.service';
import {WareBooking} from '../models/WareBooking';
import {CalendarBooking} from '../components/Ware/ware-booking-calendar/CalendarBooking';
import {Ware} from '../models/Ware';
import {colors} from '../components/Room/room-calendar/colors';
import {RoomBooking} from '../models/RoomBooking';

@Component({
  selector: 'app-ware',
  templateUrl: './ware.component.html',
  styleUrls: ['./ware.component.scss'],
})
export class WareComponent implements OnInit {
  SelectedWare: Ware;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  view: CalendarView = CalendarView.Week;
  refresh: Subject<any> = new Subject();

  constructor(private modalCtrl: ModalController, private loading: LoadingService,
              private toast: ToastService, public auth: AuthService, private wareBooking: WareBookingService) { }

  ngOnInit() {}

  loadOpeningTimeEvents = () => {

  };

  loadEvents(wareId: number, dateTime: DateTime) {
    if(this.SelectedWare == null) return;
    this.loading.Loading = true;
    this.wareBooking.AllByWareIdStartingAt(wareId, dateTime).pipe(
        map((data: WareBooking[]) => data.map(wareBooking => {
          const ret: CalendarBooking & CalendarEvent = CalendarBooking.FromWareBooking(wareBooking);
          if(wareBooking.UserId === this.auth.User.Id) return this.AddEditionProperties(ret);
          ret.color = colors.blue;
          return ret;
        })),
    ).subscribe({
      next: res => {
        this.events = res;
        this.loadOpeningTimeEvents();
      },
      error: err => {
        console.log(err);
        this.toast.PresentToast("Erreur lors du chargement des réservations");
        this.loading.Loading = false;
      },
      complete: () => this.loading.Loading = false
    });
  }

  async HourSegmentClicked(ev: {date: Date}) {
    if(ev.date.valueOf() < DateTime.local().valueOf() || ev.date.valueOf() < DateTime.local().valueOf()) return;
    const newEvent = this.CreateBaseCalendarEvent(ev);
    const wareBooking = this.InitWareBooking(newEvent);
    this.loading.Loading = true;
    this.OverlappingBookingFromOtherBooking(this.auth.User.Id, wareBooking).subscribe((otherBookings: CalendarBooking[]) => {
      if (otherBookings.length > 0) {
        this.toast.PresentToast("Vous avez déjà réservé " + otherBookings[0].Ware.Name + " pour cet horaire");
        this.loading.Loading = false;
      } else this.wareBooking.Create(wareBooking).subscribe({
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


  PreviousWeek() {
    const startWeek = startOfWeek(this.viewDate, {weekStartsOn: 1});
    let dateTime = DateTime.fromJSDate(startWeek);
    dateTime = dateTime.minus({ days: 7});
    this.loadEvents(this.SelectedWare.Id, dateTime);
    this.viewDate = dateTime.toJSDate();
  }

  TodayWeek() {
    this.viewDate = startOfWeek(new Date(), {weekStartsOn: 1});
    this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
  }

  NextWeek() {
    const startWeek = startOfWeek(this.viewDate, {weekStartsOn: 1});
    let dateTime = DateTime.fromJSDate(startWeek);
    dateTime = dateTime.plus({ days: 7});
    this.loadEvents(this.SelectedWare.Id, dateTime);
    this.viewDate = dateTime.toJSDate();
  }

  public OverlappingBookingFromOtherBooking(userId: number, wareBooking: WareBooking): Observable<CalendarBooking[]> {
    const calendarBooking = CalendarBooking.FromWareBooking(wareBooking);
    return this.wareBooking.AllOfUser(userId).pipe(
        map((data: WareBooking[]) => data.map(wareB => CalendarBooking.FromWareBooking(wareB))),
        map((bookings: CalendarBooking[]) => {
          return bookings
              .filter(b => b.id !== wareBooking.Id)
              .filter(booking => !(booking.end.valueOf() <= calendarBooking.start.valueOf()
                  || booking.start.valueOf() >= calendarBooking.end.valueOf()));
        })
    );
  }

  eventTimesChanged({event, newStart, newEnd}: CalendarEventTimesChangedEvent<CalendarBooking>) {
    if (event.start.valueOf() === newStart.valueOf() && event.end.valueOf() === newEnd.valueOf()) return;
    event.start = newStart;
    event.end = newEnd;
    const newWareBooking = this.InitWareBooking(event);
    this.loading.Loading = true;
    //TODO remove sub inside sub
    this.OverlappingBookingFromOtherBooking(this.auth.User.Id, newWareBooking).subscribe(otherBookings => {
      if (otherBookings.length > 0) {
        this.toast.PresentToast("Vous avez déjà réservé " + otherBookings[0].Ware.Name + " pour cet horaire");
        this.loading.Loading = false;
      } else this.wareBooking.Update(newWareBooking).subscribe({
        next: () => {
          this.loading.Loading = true;
          this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
        },
        error: () => {
          this.toast.PresentToast("Erreur lors de la modification");
          this.loading.Loading = false;
          this.ngOnInit();
        },
        complete: () => this.loading.Loading = false
      });
    });
  }

  private InitWareBooking(event) {
    const wareBooking = event.ToWareBooking();
    wareBooking.WareId = this.SelectedWare.Id;
    wareBooking.Ware = this.SelectedWare;
    wareBooking.User = this.auth.User;
    wareBooking.UserId = this.auth.User.Id;
    return wareBooking;
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
          this.wareBooking.Delete(event.id).subscribe({
            next: () => {
              this.loading.Loading = true;
              this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
            },
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

  WareSelected(ware: Ware) {
    this.SelectedWare = ware;
    this.loadEvents(ware.Id, DateTime.fromJSDate(this.viewDate));
  }

  FirstWareLoaded(ware: Ware) {
    if(ware == null) return;
    this.SelectedWare = ware;
    this.TodayWeek();
  }
}
