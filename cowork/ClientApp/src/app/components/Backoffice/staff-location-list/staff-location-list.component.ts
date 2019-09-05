import { Component, OnInit } from '@angular/core';
import {StaffLocationService} from '../../../services/staff-location.service';
import {StaffLocation} from '../../../models/StaffLocation';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {MealBooking} from "../../../models/MealBooking";

@Component({
  selector: 'app-staff-location-list',
  templateUrl: './staff-location-list.component.html',
  styleUrls: ['./staff-location-list.component.scss'],
})
export class StaffLocationListComponent implements OnInit {

  data: StaffLocation[];
  fields: Field[];

  constructor(private staffLocationService: StaffLocationService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "UserId", Label: "Id utilisateur", Value: null},
      { Type: "Text", Name: "PlaceId", Label: "Id espace de coworking", Value: null}
    ];
  }

  ngOnInit() {
    this.staffLocationService.GetAll().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new StaffLocation();
    model.Id = -1;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
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
      mode === "Update" ? this.staffLocationService.Update(user).subscribe(observer) : this.staffLocationService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.staffLocationService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }
}
