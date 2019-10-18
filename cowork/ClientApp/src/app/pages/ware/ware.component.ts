import {Component, OnInit} from '@angular/core';
import {addMinutes, startOfWeek} from 'date-fns';
import {DateTime} from 'luxon';
import {Observable, Subject} from 'rxjs';
import {map} from 'rxjs/operators';
import {CalendarEvent, CalendarEventTimesChangedEvent, CalendarView} from 'angular-calendar';
import {ModalController} from '@ionic/angular';
import {LoadingService} from '../../services/loading.service';
import {ToastService} from '../../services/toast.service';
import {AuthService} from '../../services/auth.service';
import {WareBookingService} from '../../services/ware-booking.service';
import {WareBooking} from '../../models/WareBooking';
import {CalendarBooking} from './CalendarBooking';
import {Ware} from '../../models/Ware';
import {colors} from './colors';
import {WareService} from '../../services/ware.service';
import {UserType} from '../../models/UserType';

@Component({
  selector: 'app-ware',
  templateUrl: './ware.component.html',
  styleUrls: ['./ware.component.scss'],
})
export class WareComponent {
  SelectedWare: Ware;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];
  view: CalendarView = CalendarView.Week;
  refresh: Subject<any> = new Subject();

  constructor(private modalCtrl: ModalController, public loading: LoadingService, public ware: WareService,
              private toast: ToastService, public auth: AuthService, private wareBooking: WareBookingService) {

  }

  ionViewWillEnter() {
    this.events = [];
    this.refresh.next();
  }

  showStaffView(event: (CalendarBooking & CalendarEvent)): (CalendarBooking & CalendarEvent) {
    event.title = "(" + event.UserId + ") " + event.User.FirstName + " " + event.User.LastName;
    return event;
  }


  loadEvents(wareId: number, dateTime: DateTime) {
    if(this.SelectedWare == null) return;
    this.loading.Loading = true;
    this.wareBooking.AllByWareIdStartingAt(wareId, dateTime).pipe(
        map((data: WareBooking[]) => data.map(wareBooking => {
          const ret: CalendarBooking & CalendarEvent = CalendarBooking.FromWareBooking(wareBooking);
          if(wareBooking.UserId === this.auth.UserId) return this.AddEditionProperties(ret);
          if(wareBooking.UserId === -1) ret.color = colors.red;
          else ret.color = colors.blue;
          if(this.auth.UserType === UserType.Staff || this.auth.UserType == UserType.Admin) {
            return this.showStaffView(ret);
          }
          return ret;
        }))
    ).subscribe({
      next: res => {
        this.events = res;
      },
      error: err => {
        this.toast.PresentToast("Erreur lors du chargement des réservations");
        this.loading.Loading = false;
      },
      complete: () => this.loading.Loading = false
    });
  }

  async HourSegmentClicked(ev: {date: Date}) {
    if(this.SelectedWare == null) {
      this.toast.PresentToast("Aucun matériel selectionné");
      return;
    }
    if(ev.date.valueOf() < DateTime.local().valueOf() || ev.date.valueOf() < DateTime.local().valueOf()) return;
    const newEvent = this.CreateBaseCalendarEvent(ev);
    const wareBooking = this.InitWareBooking(newEvent);
    this.loading.Loading = true;
    this.wareBooking.Create(wareBooking).subscribe({
      next: res => {
        if (res !== -1) {
          newEvent.id = res;
          this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
        } else this.toast.PresentToast("Erreur lors de la création de la réservation");
      },
      error: () => {
        this.toast.PresentToast("Erreur lors de l'ajout de la réservation");
        this.loading.Loading = false;
        this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
      },
      complete: () => this.loading.Loading = false
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


  eventTimesChanged({event, newStart, newEnd}: CalendarEventTimesChangedEvent<CalendarBooking>) {
    if (event.start.valueOf() === newStart.valueOf() && event.end.valueOf() === newEnd.valueOf()) return;
    event.start = newStart;
    event.end = newEnd;
    const newWareBooking = this.InitWareBooking(event);
    this.loading.Loading = true;
    this.wareBooking.Update(newWareBooking).subscribe({
      next: () => {
        this.loading.Loading = true;
        this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
      },
      error: () => {
        this.toast.PresentToast("Erreur lors de la modification");
        this.loading.Loading = false;
        this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
      },
      complete: () => this.loading.Loading = false
    });
  }

  private InitWareBooking(event) {
    const wareBooking = event.ToWareBooking();
    wareBooking.WareId = this.SelectedWare.Id;
    wareBooking.Ware = this.SelectedWare;
    wareBooking.User = this.auth.User;
    wareBooking.UserId = this.auth.UserId;
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
              this.loadEvents(this.SelectedWare.Id, DateTime.fromJSDate(this.viewDate));
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

  Refresh() {
    this.refresh.next();
  }
}
