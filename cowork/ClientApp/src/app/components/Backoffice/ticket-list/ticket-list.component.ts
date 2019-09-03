import { Component, OnInit } from '@angular/core';
import {Ticket} from '../../../models/Ticket';
import {TicketService} from '../../../services/ticket.service';
import {TicketState} from '../../../models/TicketState';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {

  data: Ticket[];

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.All().subscribe(res => this.data = res);
  }

  GetTicketState(ind: number) {
    return TicketState[ind];
  }

}
