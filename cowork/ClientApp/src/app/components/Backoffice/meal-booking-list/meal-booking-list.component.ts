import { Component, OnInit } from '@angular/core';
import {MealBooking} from '../../../models/MealBooking';
import {MealBookingService} from '../../../services/meal-booking.service';
import {ModalController} from "@ionic/angular";
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {User} from "../../../models/User";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {MealService} from "../../../services/meal.service";

@Component({
  selector: 'app-meal-booking-list',
  templateUrl: './meal-booking-list.component.html',
  styleUrls: ['./meal-booking-list.component.scss'],
})
export class MealBookingListComponent implements OnInit {
    data: MealBooking[];
    fields: Field[];

  constructor(private mealBookingService: MealBookingService, private mealService: MealService, private modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "MealId", Label: "Id repas", Value: 0 },
      { Type: "Text", Name: "UserId", Label: "Id utilisateur", Value: 0},
    ];
   
  }

  ngOnInit() {
    this.mealBookingService.All().subscribe(res => this.data = res);
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new MealBooking();
    model.Id = -1;
    model.MealId = fieldDic["MealId"][0].Value as number;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.Note = "";
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
      this.mealBookingService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
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
