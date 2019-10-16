import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Room} from '../../models/Room';
import {RoomService} from '../../services/room.service';
import {LoadingService} from '../../services/loading.service';
import {ToastService} from '../../services/toast.service';

@Component({
  selector: 'app-tab3',
  templateUrl: 'reservation.component.html',
  styleUrls: ['reservation.component.scss']
})
export class ReservationTabPage implements OnInit {
  public rooms: Room[];

  constructor(public auth: AuthService, public loading: LoadingService,
              public roomService: RoomService, public toast: ToastService) {
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
      this.Refresh();
  }

  Refresh() {
    this.loading.Loading = true;
    this.roomService.AllFromPlace(this.auth.PlaceId).subscribe({
      next: res => this.rooms = res,
      error: () => {
        this.toast.PresentToast("Une erreur est survenue lors du chargement des salles")
        this.loading.Loading = false;
      },
      complete: () => this.loading.Loading = false
    });
  }
}
