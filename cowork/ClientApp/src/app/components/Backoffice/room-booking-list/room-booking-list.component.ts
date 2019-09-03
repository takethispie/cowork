import { Component, OnInit } from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';
import {RoomBookingService} from '../../../services/room-booking.service';

@Component({
  selector: 'app-room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.scss'],
})
export class RoomBookingListComponent implements OnInit {

  data: RoomBooking[];

  constructor(public roomBookingService: RoomBookingService) { }

  ngOnInit() {
    this.roomBookingService.All().subscribe(res => this.data = res);
  }

}
