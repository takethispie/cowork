import {Component, Input, OnInit} from '@angular/core';
import {Room} from '../../../models/Room';
import {ModalController} from '@ionic/angular';
import {RoomCalendarComponent} from '../room-calendar/room-calendar.component';
import {RoomType} from '../../../models/RoomType';
import {LoadingService} from '../../../services/loading.service';

@Component({
  selector: 'room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.scss'],
})
export class RoomListComponent implements OnInit {

  @Input() Rooms: Room[] = [];

  constructor(public modal: ModalController, public loading: LoadingService) {
  }

  ionViewWillEnter() {
  }

  ngOnInit() {}

  async RoomSelected(item: Room) {
    this.loading.Loading = true;
    const modal = await this.modal.create({
      component: RoomCalendarComponent,
      componentProps: { room: item },
      backdropDismiss: false,
      cssClass: ["my-custom-modal-css"]
    });
    modal.present();
  }

  GetRoomType(type: number) {
    return RoomType[type];
  }
}
