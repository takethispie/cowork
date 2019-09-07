import {Component, OnInit} from '@angular/core';
import {WareBooking} from '../../../models/WareBooking';
import {WareBookingService} from '../../../services/ware-booking.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DateTime} from 'luxon';
import {ModalController} from '@ionic/angular';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ware-booking-list',
  templateUrl: './ware-booking-list.component.html',
  styleUrls: ['./ware-booking-list.component.scss'],
})
export class WareBookingListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<WareBooking>;
  
  constructor(private wareBookingService: WareBookingService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "UserId", "Id de l'utilisateur", -1),
      new Field(FieldType.Number, "WareId", "Id du matériel", -1),
      new Field(FieldType.DateTimePicker, "Start", "Début", DateTime.local().toISO()),
      new Field(FieldType.DateTimePicker, "End", "Fin", DateTime.local().plus({minutes: 30}).toISO()),
    ];
    this.dataHandler = new TableDataHandler<WareBooking>(this.wareBookingService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new WareBooking();
    model.Id = fieldDic["Id"][0].Value as number;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.WareId = fieldDic["WareId"][0].Value as number;
    model.Start = DateTime.fromISO(fieldDic["Start"][0].Value as string);
    model.End = DateTime.fromISO(fieldDic["End"][0].Value as string);
    return model;
  }
}
