import {Component, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {ModalController} from '@ionic/angular';
import {Ticket} from '../../../models/Ticket';
import {TicketState} from '../../../models/TicketState';
import {DateTime} from 'luxon';

@Component({
  selector: 'app-add-ticket',
  templateUrl: './add-ticket.component.html',
  styleUrls: ['./add-ticket.component.scss'],
})
export class AddTicketComponent implements OnInit {
    title: string;

  constructor(private addTicketModalCtrl: ModalController) { }

  ngOnInit() {

  }

  CreateTicket(form: NgForm) {
    const ticket = new Ticket();
    ticket.Title = form.value.title;
    ticket.Description = form.value.description;
    ticket.State = TicketState.New;
    ticket.Created = DateTime.local();
    this.addTicketModalCtrl.dismiss(ticket);
  }

    GoBack() {
        this.addTicketModalCtrl.dismiss(null);
    }
}
