import {Component, OnInit} from '@angular/core';
import {MealBooking} from '../../../models/MealBooking';
import {MealBookingService} from '../../../services/meal-booking.service';
import {ModalController} from '@ionic/angular';
import List from 'linqts/dist/src/list';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-meal-booking-list',
  templateUrl: './meal-booking-list.component.html',
  styleUrls: ['./meal-booking-list.component.scss'],
})
export class MealBookingListComponent implements OnInit {
  fields: Field[];
  dataHandler: TableDataHandler<MealBooking>;

  constructor(private mealBookingService: MealBookingService, private modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "MealId", "Id Meal", -1),
      new Field(FieldType.Number, "UserId", "Id Utilisateur", -1)
    ];
    this.dataHandler = new TableDataHandler<MealBooking>(this.mealBookingService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new MealBooking();
    model.Id = fieldDic["Id"][0].Value as number;
    model.MealId = fieldDic["MealId"][0].Value as number;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.Note = "";
    return model;
  }
}
