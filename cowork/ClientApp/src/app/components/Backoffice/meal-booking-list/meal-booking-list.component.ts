import { Component, OnInit } from '@angular/core';
import {MealBooking} from '../../../models/MealBooking';
import {MealBookingService} from '../../../services/meal-booking.service';
import {ModalController} from "@ionic/angular";
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";

@Component({
  selector: 'app-meal-booking-list',
  templateUrl: './meal-booking-list.component.html',
  styleUrls: ['./meal-booking-list.component.scss'],
})
export class MealBookingListComponent implements OnInit {
    data: MealBooking[];
    fields: Field[];

  constructor(private mealBookingService: MealBookingService, private modalCtrl: ModalController) {
    this.fields = [
      { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: "-1"},
      { Type: "Number", Name: "MealId", Label: "Id repas", Value: 0 },
      { Type: "Number", Name: "UserId", Label: "Id utilisateur", Value: 0},
    ];
   
  }

  ngOnInit() {
    this.mealBookingService.All().subscribe(res => this.data = res);
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new MealBooking();
    model.Id = fieldDic["Id"][0].Value as number;
    model.MealId = fieldDic["MealId"][0].Value as number;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.Note = "";
    return model;
  }

  async UpdateItem(model: MealBooking, fields: Field[]) {
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
      mode === "Update" ? this.mealBookingService.Update(user).subscribe(observer) : this.mealBookingService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.mealBookingService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }
}
