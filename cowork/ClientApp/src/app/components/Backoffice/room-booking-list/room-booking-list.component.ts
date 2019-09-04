import { Component, OnInit } from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';
import {RoomBookingService} from '../../../services/room-booking.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";

@Component({
  selector: 'app-room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.scss'],
})
export class RoomBookingListComponent implements OnInit {

  data: RoomBooking[];
  fields: Field[];

  constructor(public roomBookingService: RoomBookingService, public modalCtrl: ModalController) { 
    this.fields = [
      { Type: "Text", Name: "ClientId", Label: "Id utilisateur", Value: null},
      { Type: "Text", Name: "RoomId", Label: "Id de la salle", Value: null},
      { Type: "DateTimePicker", Name: "Start", Label: "Début de la réservation", Value: ""},
      { Type: "DateTimePicker", Name: "End", Label: "Fin de la réservation", Value: ""}
    ];
  }

  ngOnInit() {
    this.roomBookingService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new RoomBooking();
    model.Id = -1;
    model.ClientId = fieldDic["ClientId"][0].Value as number;
    model.RoomId = fieldDic["RoomId"][0].Value as number;
    model.Start = DateTime.fromISO(fieldDic["Start"][0].Value as string);
    model.End = DateTime.fromISO(fieldDic["End"][0].Value as string);
    return model;
  }

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const model = this.CreateModelFromFields(this.fields);
      this.roomBookingService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.roomBookingService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
