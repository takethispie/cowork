import {Component, OnInit} from '@angular/core';
import {TicketAttribution} from '../../../models/TicketAttribution';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {TicketAttributionService} from '../../../services/ticket-attribution.service';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ticket-attribution-list',
  templateUrl: './ticket-attribution-list.component.html',
  styleUrls: ['./ticket-attribution-list.component.scss'],
})
export class TicketAttributionListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<TicketAttribution>;
  
  constructor(public ApiService: TicketAttributionService, public modalCtrl: ModalController) { 
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "StaffId", "Id du personnel", -1),
      new Field(FieldType.Number, "TicketId", "Id du ticket", -1)
    ];
    this.dataHandler = new TableDataHandler<TicketAttribution>(this.ApiService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }


  ngOnInit() {
    this.dataHandler.Refresh();
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new TicketAttribution();
    model.Id = fieldDic["Id"][0].Value as number;
    model.StaffId = fieldDic["StaffId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    return model;
  }
}
