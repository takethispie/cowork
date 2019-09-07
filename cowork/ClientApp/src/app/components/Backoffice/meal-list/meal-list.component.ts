import {Component, OnInit} from '@angular/core';
import {Meal} from '../../../models/Meal';
import {MealService} from '../../../services/meal.service';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {DateTime} from 'luxon';
import {Field, FieldType} from '../../dynamic-form-builder/Field';

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.scss'],
})
export class MealListComponent implements OnInit {
  data: Meal[];
  fields: Field[];
  page: number = 0;
  amount: number = 30;

  constructor(private mealService: MealService, private modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.DatePicker, "Date", "Date", DateTime.local().toISODate()),
      new Field(FieldType.Text, "Description", "Description", ""),
      new Field(FieldType.Number, "PlaceId", "Id de l'espace de coworking", -1)
    ]
  }

  ngOnInit() {
    this.Refresh();
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


  Refresh() {
    this.page = 0;
    this.data = [];
    this.loadData(null);
  }


  loadData(event: any) {
    this.mealService.AllWithPaging(this.page, this.amount).subscribe({
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
