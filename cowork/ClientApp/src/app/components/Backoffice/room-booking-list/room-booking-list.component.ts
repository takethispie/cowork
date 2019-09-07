import {Component, OnInit} from '@angular/core';
import {RoomBooking} from '../../../models/RoomBooking';
import {RoomBookingService} from '../../../services/room-booking.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {DateTime} from 'luxon';

@Component({
  selector: 'app-room-booking-list',
  templateUrl: './room-booking-list.component.html',
  styleUrls: ['./room-booking-list.component.scss'],
})
export class RoomBookingListComponent implements OnInit {

  data: RoomBooking[];
  fields: Field[];
  private page: number;
  private amount: number;

  constructor(public roomBookingService: RoomBookingService, public modalCtrl: ModalController) { 
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "ClientId", "Id de l'utilisateur", -1),
      new Field(FieldType.Number, "RoomId", "Id de la salle", -1),
      new Field(FieldType.DateTimePicker, "Start", "Début de la réservation", DateTime.local().toISO()),
      new Field(FieldType.DateTimePicker, "End", "Fin de la réservation", DateTime.local().plus({ minutes: 30}).toISO()),
    ];
  }

  ngOnInit() {
    this.Refresh();
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


  Refresh() {
    this.page = 0;
    this.data = [];
    this.loadData(null);
  }


  loadData(event: any) {
    this.roomBookingService.AllWithPaging(this.page, this.amount).subscribe({
      next: value => {
        if(value.length === 0) return;
        this.data = this.data.concat(value);
        this.page++;
      },
      error: err => {
        console.log(err);
        if(event != null)  event.target.complete();
      },
      complete: () => { if(event != null)  event.target.complete(); }
    });
  }

}
