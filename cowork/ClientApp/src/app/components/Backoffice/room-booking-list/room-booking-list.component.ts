import { Component, OnInit } from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';
import {RoomBookingService} from '../../../services/room-booking.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";
import {MealBooking} from "../../../models/MealBooking";

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
      { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: "-1"},
      { Type: "Number", Name: "ClientId", Label: "Id utilisateur", Value: null},
      { Type: "Number", Name: "RoomId", Label: "Id de la salle", Value: null},
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
    model.Id = fieldDic["Id"][0].Value as number;
    model.ClientId = fieldDic["ClientId"][0].Value as number;
    model.RoomId = fieldDic["RoomId"][0].Value as number;
    model.Start = DateTime.fromISO(fieldDic["Start"][0].Value as string);
    model.End = DateTime.fromISO(fieldDic["End"][0].Value as string);
    return model;
  }

  async UpdateItem(model: RoomBooking, fields: Field[]) {
    const fieldList = new List(fields).Select(field => {
      field.Value = model[field.Name];
      return field;
    });
    await this.OpenModal("Update", { Fields: fieldList.ToArray()});
  }

  async AddItem() {
    await this.OpenModal("Create", { Fields: this.fields });
  }

  async OpenModal(mode: "Update" | "Create" ,componentProps: any) {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const user = this.CreateModelFromFields(this.fields);
      const observer = {
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      };
      mode === "Update" ? this.roomBookingService.Update(user).subscribe(observer) : this.roomBookingService.Create(user).subscribe(observer);
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
