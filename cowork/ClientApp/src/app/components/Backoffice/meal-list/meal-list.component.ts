import { Component, OnInit } from '@angular/core';
import {Meal} from '../../../models/Meal';
import {MealService} from '../../../services/meal.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";

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

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const model = this.CreateModelFromFields(this.fields);
      this.mealService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
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
