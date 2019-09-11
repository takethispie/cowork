import {Component, OnInit} from '@angular/core';
import {Ticket} from '../models/Ticket';
import {AuthService} from '../services/auth.service';
import {TicketService} from '../services/ticket.service';
import {ModalController} from '@ionic/angular';
import {AddTicketComponent} from '../components/Ticket/add-ticket/add-ticket.component';
import {SubscriptionService} from '../services/subscription.service';
import {flatMap} from 'rxjs/operators';
import {of} from 'rxjs';
import {ToastService} from '../services/toast.service';
import {LoadingService} from '../services/loading.service';
import {NgForm} from '@angular/forms';
import {TicketState} from '../models/TicketState';
import {DateTime} from 'luxon';


@Component({
  selector: 'app-ticket',
  templateUrl: 'ticket.component.html',
  styleUrls: ['ticket.component.scss']
})
export class TicketComponent implements OnInit {
  public Tickets: Ticket[];
  placeId: number;
  userId: number;

  constructor(public auth: AuthService, private ticketService: TicketService, public toast: ToastService,
              private pageModal: ModalController, private subService: SubscriptionService, public load: LoadingService) {
    this.Tickets = [];
  }

  ngOnInit() {
      this.load.Loading = true;
      this.userId = this.auth.UserId;
      this.ticketService.OpenedBy(this.auth.UserId).subscribe(res => {
          this.Tickets = res;
          this.load.Loading = false;
      });

      this.placeId = this.auth.PlaceId;
  }


  ionViewWillEnter() {
    this.ngOnInit();
  }

  CreateTicket(form: NgForm) {
    const ticket = new Ticket();
    ticket.Title = form.value.title;
    ticket.Description = form.value.description;
    ticket.State = TicketState.New;
    ticket.Created = DateTime.local();
    this.OpenNewTicket(ticket);
  }

  OpenNewTicket(ticket: Ticket) {
      if(ticket == null) return;
      ticket.OpendedBy = this.auth.User;
      ticket.OpenedById = this.auth.UserId;
      this.subService.OfUser(this.auth.UserId).pipe(
          flatMap(sub => {
            if (sub == null) return of(null);
            ticket.Place = sub.Place;
            ticket.PlaceId = sub.Place.Id;
            return this.ticketService.Create(ticket)
          })
      ).subscribe({
        next: id => {
          if (id === -1) this.toast.PresentToast("Impossible d'ajouter le ticket");
          else this.ngOnInit();
        },
        error: () => {
          this.toast.PresentToast("Erreur lors de l'ajout");
          this.load.Loading = false;
        }
      });
  }
}
