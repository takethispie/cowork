import {Component, OnInit} from '@angular/core';
import {TicketState} from '../../../models/TicketState';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {TicketWareService} from '../../../services/ticket-ware.service';
import {ModalController} from '@ionic/angular';
import {TicketWare} from '../../../models/TicketWare';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ticket-ware-list',
  templateUrl: './ticket-ware-list.component.html',
  styleUrls: ['./ticket-ware-list.component.scss'],
})
export class TicketWareListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<TicketWare>;

  constructor(public ticketWareService: TicketWareService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "TicketId", "Id du ticket", -1),
      new Field(FieldType.Number, "WareId", "Id du matériel", -1)
    ];
    this.dataHandler = new TableDataHandler<TicketWare>(this.ticketWareService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }


  ngOnInit() {
    this.dataHandler.Refresh();
  }

  GetTicketState(ind: number) {
    return TicketState[ind];
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new TicketWare();
    model.Id = fieldDic["Id"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    model.WareId = fieldDic["WareId"][0].Value as number;
    return model;
  }
}
