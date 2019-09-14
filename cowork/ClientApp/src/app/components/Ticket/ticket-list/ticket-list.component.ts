import {Component, Input, OnInit} from '@angular/core';
import {Ticket} from '../../../models/Ticket';
import {User} from '../../../models/User';
import {AuthService} from '../../../services/auth.service';
import {TicketService} from '../../../services/ticket.service';
import {ToastService} from '../../../services/toast.service';
import {LoadingService} from '../../../services/loading.service';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {

  @Input() Tickets: Ticket[];
  User: User;

  constructor(public auth: AuthService, public ticketService: TicketService, public  toastService: ToastService, public  loadbar: LoadingService) {
    this.User = auth.User;
  }

  ngOnInit() {}

  Delete(ticketId: number) {
    this.loadbar.Loading = true;
    this.ticketService.Delete(ticketId).subscribe({
      next: value => {
        this.Tickets = this.Tickets.filter(ticket => ticket.Id !== ticketId);
      },
      error: (err: HttpErrorResponse) => {
        this.loadbar.Loading = false;
        this.toastService.PresentToast("Une erreur est survenue lors de la suppression du ticket");
      },
      complete: () => this.loadbar.Loading = false
  });
  }
}
