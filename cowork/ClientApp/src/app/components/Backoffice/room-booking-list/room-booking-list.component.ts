import {Component, OnInit} from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';
import {RoomBookingService} from '../../../services/room-booking.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {DateTime} from 'luxon';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.scss'],
})
export class RoomBookingListComponent implements OnInit {
  fields: Field[];
  dataHandler: TableDataHandler<RoomBooking>;

  constructor(public roomBookingService: RoomBookingService, public modalCtrl: ModalController) { 
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "ClientId", "Id de l'utilisateur", -1),
      new Field(FieldType.Number, "RoomId", "Id de la salle", -1),
      new Field(FieldType.DateTimePicker, "Start", "Début de la réservation", DateTime.local().toISO()),
      new Field(FieldType.DateTimePicker, "End", "Fin de la réservation", DateTime.local().plus({ minutes: 30}).toISO()),
    ];
    this.dataHandler = new TableDataHandler<RoomBooking>(this.roomBookingService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new RoomBooking();
    model.Id = fieldDic["Id"][0].Value as number;
    model.ClientId = fieldDic["ClientId"][0].Value as number;
    model.RoomId = fieldDic["RoomId"][0].Value as number;
    model.Start = DateTime.fromISO(fieldDic["Start"][0].Value as string);
    model.End = DateTime.fromISO(fieldDic["End"][0].Value as string);
    return model;
  }
}
