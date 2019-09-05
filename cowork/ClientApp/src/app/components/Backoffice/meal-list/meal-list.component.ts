import { Component, OnInit } from '@angular/core';
import {Meal} from '../../../models/Meal';
import {MealService} from '../../../services/meal.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";
import {MealBooking} from "../../../models/MealBooking";

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.scss'],
})
export class MealListComponent implements OnInit {
    data: Meal[];
    fields: Field[];

  constructor(private mealService: MealService, private modalCtrl: ModalController) {
    this.fields = [
      { Type: "DatePicker", Name: "Date", Value: DateTime.local().toISODate(), Label: "Date"},
      { Type: "Text", Name: "Description", Label: "Description", Value: ""},
      { Type: "Text", Name: "PlaceId", Label: "Id espace de coworking", Value: 0}
    ]
  }

  ngOnInit() {
    this.mealService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Meal();
    model.Id = -1;
    model.Date = DateTime.fromISO(fieldDic["Date"][0].Value as string);
    model.Description = fieldDic["Description"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    return model;
  }

  async UpdateItem(model: Meal, fields: Field[]) {
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
      mode === "Update" ? this.mealService.Update(user).subscribe(observer) : this.mealService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.mealService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }
}
