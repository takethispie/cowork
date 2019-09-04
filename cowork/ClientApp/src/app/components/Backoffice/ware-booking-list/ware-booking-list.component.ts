import { Component, OnInit } from '@angular/core';
import {WareBooking} from '../../../models/WareBooking';
import {WareBookingService} from '../../../services/ware-booking.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {Meal} from "../../../models/Meal";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-ware-booking-list',
  templateUrl: './ware-booking-list.component.html',
  styleUrls: ['./ware-booking-list.component.scss'],
})
export class WareBookingListComponent implements OnInit {

  data: WareBooking[];
  fields: Field[];
  
  constructor(private wareBookingService: WareBookingService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "UserId", Label: "Id de l'utilisateur", Value: null},
      { Type: "Text", Name: "WareId", Label: "Id du matériel", Value: null},
      { Type: "DateTimePicker", Name: "Start", Label: "Début", Value: DateTime.local().toISO()},
      { Type: "DateTimePicker", Name: "End", Label: "Fin", Value: DateTime.local().plus({"minutes": 30}).toISO()}  
    ];
  }

  ngOnInit() {
    this.wareBookingService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new WareBooking();
    model.Id = -1;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.WareId = fieldDic["WareId"][0].Value as number;
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
      this.wareBookingService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.wareBookingService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
