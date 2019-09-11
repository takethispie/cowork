import {Component, OnInit} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {SubscriptionService} from '../services/subscription.service';
import {Subscription} from '../models/Subscription';
import {RoomBookingService} from '../services/room-booking.service';
import {RoomBooking} from '../models/RoomBooking';
import {Room} from '../models/Room';
import {RoomService} from '../services/room.service';
import {RoomType} from '../models/RoomType';
import {LoadingService} from '../services/loading.service';
import {ToastService} from '../services/toast.service';

@Component({
  selector: 'app-tab3',
  templateUrl: 'reservation.component.html',
  styleUrls: ['reservation.component.scss']
})
export class ReservationTabPage implements OnInit {
  private userSub: Subscription;
  private bookedRooms: RoomBooking[];
  public rooms: Room[];

  constructor(public auth: AuthService, public subService: SubscriptionService, public loading: LoadingService,
              public roomService: RoomService, public toast: ToastService) {}

  ngOnInit() {
    this.loading.Loading = true;
    this.subService.OfUser(this.auth.UserId).subscribe({
      next: res => {
        if(res == null) return;
        this.userSub = res;
        this.roomService.AllFromPlace(this.userSub.Place.Id).subscribe(rooms => this.rooms = rooms);
      },
      error: () => this.toast.PresentToast("Une erreur est survenue lors du chargement des salles"),
      complete: () => this.loading.Loading = false
    });
  }

  ionViewWillEnter() {
      this.Refresh();
  }

  Refresh() {
    if(this.userSub == null) return;
    this.loading.Loading = true;
    this.roomService.AllFromPlace(this.userSub.Place.Id).subscribe({
      next: res => this.rooms = res,
      error: () => this.toast.PresentToast("Une erreur est survenue lors du chargement des salles"),
      complete: () => this.loading.Loading = false
    });
  }
}
