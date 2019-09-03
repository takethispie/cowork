import { Component, OnInit } from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {TicketService} from '../../../services/ticket.service';

@Component({
  selector: 'app-ticket-comment-list',
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.scss'],
})
export class TicketCommentListComponent implements OnInit {

  data: TicketComment[];

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.AllComments().subscribe(res => this.data = res);
  }

}
