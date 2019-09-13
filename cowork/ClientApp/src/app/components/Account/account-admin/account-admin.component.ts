import { Component, OnInit } from '@angular/core';
import {LoadingService} from '../../../services/loading.service';
import {TicketService} from '../../../services/ticket.service';
import {MealBookingService} from '../../../services/meal-booking.service';
import {RoomBookingService} from '../../../services/room-booking.service';
import {WareBookingService} from '../../../services/ware-booking.service';
import {Ticket} from '../../../models/Ticket';

@Component({
  selector: 'account-admin',
  templateUrl: './account-admin.component.html',
  styleUrls: ['./account-admin.component.scss'],
})
export class AccountAdminComponent implements OnInit {

  public Tickets: Ticket[]

  constructor(public loader: LoadingService, public ticketService: TicketService, public roomBooking: RoomBookingService,
              public mealBooking: MealBookingService, public wareBooking: WareBookingService) {

  }

  ngOnInit() {}


}
