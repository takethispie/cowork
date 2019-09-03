import { Component, OnInit } from '@angular/core';
import {WareBooking} from '../../../models/WareBooking';
import {WareBookingService} from '../../../services/ware-booking.service';

@Component({
  selector: 'app-ware-booking-list',
  templateUrl: './ware-booking-list.component.html',
  styleUrls: ['./ware-booking-list.component.scss'],
})
export class WareBookingListComponent implements OnInit {

  data: WareBooking[];

  constructor(private wareBookingService: WareBookingService) { }

  ngOnInit() {
    this.wareBookingService.All().subscribe(res => this.data = res);
  }

}
