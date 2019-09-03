import { Component, OnInit } from '@angular/core';
import {TicketAttribution} from '../../../models/TicketAttribution';
import {TicketService} from '../../../services/ticket.service';

@Component({
  selector: 'app-ticket-attribution-list',
  templateUrl: './ticket-attribution-list.component.html',
  styleUrls: ['./ticket-attribution-list.component.scss'],
})
export class TicketAttributionListComponent implements OnInit {

  data: TicketAttribution[];

  constructor(public ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.AllAttribution().subscribe(res => this.data = res);
  }

}
