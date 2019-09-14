import {Component, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {ModalController} from '@ionic/angular';
import {Ticket} from '../../../models/Ticket';
import {TicketState} from '../../../models/TicketState';
import {DateTime} from 'luxon';
import {AuthService} from '../../../services/auth.service';
import {catchError, flatMap, map} from 'rxjs/operators';
import {of} from 'rxjs';
import {SubscriptionService} from '../../../services/subscription.service';
import {TicketService} from '../../../services/ticket.service';
import {ToastService} from '../../../services/toast.service';
import {LoadingService} from '../../../services/loading.service';
import {Ware} from '../../../models/Ware';
import {WareListModalComponent} from '../../Ware/ware-list-modal/ware-list-modal.component';
import {TicketWareService} from '../../../services/ticket-ware.service';
import {TicketWare} from '../../../models/TicketWare';
import {UserType} from '../../../models/UserType';
import {tick} from '@angular/core/testing';

@Component({
  selector: 'app-add-ticket',
  templateUrl: './add-ticket.component.html',
  styleUrls: ['./add-ticket.component.scss'],
})
export class AddTicketComponent implements OnInit {
    public title: string;
    public SelectedWare: Ware;

  constructor(private addTicketModalCtrl: ModalController, public auth: AuthService, public subService: SubscriptionService,
              public ticketService: TicketService, public toast: ToastService, public load: LoadingService,
              public ticketWareService: TicketWareService) { }

  ngOnInit() {

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
    if(this.auth.UserType === UserType.Staff) ticket.Title = "[Staff] " + ticket.Title;
    this.subService.OfUser(this.auth.UserId).pipe(
        flatMap(sub => {
          if (sub == null) return of(null);
          ticket.Place = sub.Place;
          ticket.PlaceId = sub.Place.Id;
          const ticketWare = new TicketWare();
          if(this.SelectedWare == null) return this.ticketService.Create(ticket);
          ticketWare.WareId = this.SelectedWare.Id;
          return this.ticketService.Create(ticket).pipe(
              flatMap(res => {
                  if(res === -1) return of(-1);
                  ticketWare.TicketId = res;
                  return this.ticketWareService.Create(ticketWare).pipe(
                      catchError((err, caught) => this.ticketService.Delete(res).pipe(map(x => -1)))
                  );
              })
          )
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

  async OpenWareSelection() {
    const modal = await this.addTicketModalCtrl.create({
      component: WareListModalComponent,
      componentProps: { PlaceId: this.auth.PlaceId, UserId: this.auth.UserId }
    });
    modal.onDidDismiss().then(res => {
      if(res.data != null) this.SelectedWare = res.data;
    });
    modal.present();
  }

  RemoveAttributedWare() {
    this.SelectedWare = null;
  }
}
