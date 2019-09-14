import {Component, Input, OnInit} from '@angular/core';
import {Ticket} from '../../../models/Ticket';
import {AuthService} from '../../../services/auth.service';
import {TicketService} from '../../../services/ticket.service';
import {TicketState} from '../../../models/TicketState';
import {LoadingService} from '../../../services/loading.service';
import {ToastService} from '../../../services/toast.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'account-staff',
  templateUrl: './account-staff.component.html',
  styleUrls: ['./account-staff.component.scss'],
})
export class AccountStaffComponent implements OnInit {

  public Tickets: Ticket[];
  @Input() Refresher: Observable<object>;

  constructor(public auth: AuthService, public ticketService: TicketService, public loading: LoadingService, public toast: ToastService) {
  }

  ngOnInit() {
    this.Refresher.subscribe({
      next: value => this.LoadData(),
      error: err => this.toast.PresentToast("Erreur lors du chargement des tickets")
    })
  }


  public LoadData() {
    this.loading.Loading = true;
    this.ticketService.AllWithState(TicketState.New).subscribe({
      next: res => {
        this.Tickets = res;
        this.loading.Loading = false;
      },
      error: err => {
        this.toast.PresentToast("Une erreur est survenu lors du chargement des tickets ouverts");
        this.loading.Loading = false;
      }
    });
  }

}
