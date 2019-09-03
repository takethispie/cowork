import {Component, Input, OnInit} from '@angular/core';
import {RoomBookingService} from '../../../services/room-booking.service';
import {AuthService} from '../../../services/auth.service';
import {ModalController} from '@ionic/angular';

@Component({
  selector: 'add-room-booking',
  templateUrl: './add-room-booking.component.html',
  styleUrls: ['./add-room-booking.component.scss'],
})
export class AddRoomBookingComponent implements OnInit {
  @Input() EventDate: string;

  constructor(public roomBookingService: RoomBookingService, public auth: AuthService, private modal: ModalController) { }

  ngOnInit() {
    console.log(this.EventDate);
  }

  GoBack() {
    this.modal.dismiss(null);
  }
}
