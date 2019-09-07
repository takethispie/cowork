import {Component, OnInit} from '@angular/core';
import {Ticket} from '../../../models/Ticket';
import {TicketService} from '../../../services/ticket.service';
import {TicketState} from '../../../models/TicketState';
import List from 'linqts/dist/src/list';
import {DateTime} from 'luxon';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<Ticket>;

  constructor(private ticketService: TicketService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "Title", "Titre", ""),
      new Field(FieldType.Text, "Description", "Description", ""),
      new Field(FieldType.Number, "AttributedToId", "Id du personnel auquel est attribué le ticket", -1),
      new Field(FieldType.DateTimePicker, "Created", "Date de création du ticket", DateTime.local().toISO()),
      new Field(FieldType.Number, "UserId", "Id utilisateur", -1),
      new Field(FieldType.Number, "PlaceId", "Id espace de coworking", -1),
      new Field(FieldType.DateTimePicker, "PlanifiedResolution", "Date planifiée de résolution du ticket", DateTime.local().toISO()),
      new Field(FieldType.Select, "State", "Status du ticket", 0,  [
          { Label: TicketState[0], Value: 0},
          { Label: TicketState[1], Value: 1},
          { Label: TicketState[2], Value: 2},
          { Label: TicketState[3], Value: 3},
          { Label: TicketState[4], Value: 4}
        ]
      )
    ];
    this.dataHandler = new TableDataHandler<Ticket>(this.ticketService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  GetTicketState(ind: number) {
    return TicketState[ind];
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Ticket();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Title = fieldDic["Title"][0].Value as string;
    model.Description = fieldDic["Description"][0].Value as string;
    model.Created = DateTime.local();
    model.OpenedById = fieldDic["UserId"][0].Value as number;
    model.State = fieldDic["State"][0].Value as number;
    return model;
  }
}
