import {Component, OnInit} from '@angular/core';
import {Meal} from '../../../models/Meal';
import {MealService} from '../../../services/meal.service';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {DateTime} from 'luxon';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.scss'],
})
export class MealListComponent implements OnInit {
  fields: Field[];
  dataHandler: TableDataHandler<Meal>;

  constructor(private mealService: MealService, private modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.DatePicker, "Date", "Date", DateTime.local().toISODate()),
      new Field(FieldType.Text, "Description", "Description", ""),
      new Field(FieldType.Number, "PlaceId", "Id de l'espace de coworking", -1)
    ];
    this.dataHandler = new TableDataHandler<Meal>(this.mealService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Meal();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Date = DateTime.fromISO(fieldDic["Date"][0].Value as string);
    model.Description = fieldDic["Description"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    return model;
  }
}
