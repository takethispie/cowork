import {Component, Input, OnInit} from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';

@Component({
  selector: 'room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.scss'],
})
export class RoomBookingListComponent implements OnInit {
  @Input() RoomBookings: RoomBooking[];

  constructor() { }

  ngOnInit() {}

}
