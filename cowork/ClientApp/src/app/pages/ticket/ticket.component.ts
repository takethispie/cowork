import {Component, OnInit} from '@angular/core';
import {Ticket} from '../../models/Ticket';
import {AuthService} from '../../services/auth.service';
import {TicketService} from '../../services/ticket.service';
import {ModalController} from '@ionic/angular';
import {SubscriptionService} from '../../services/subscription.service';
import {ToastService} from '../../services/toast.service';
import {LoadingService} from '../../services/loading.service';


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
}
