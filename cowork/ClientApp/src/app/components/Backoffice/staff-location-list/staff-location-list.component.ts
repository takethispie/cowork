import { Component, OnInit } from '@angular/core';
import {StaffLocationService} from '../../../services/staff-location.service';
import {StaffLocation} from '../../../models/StaffLocation';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

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

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const model = this.CreateModelFromFields(this.fields);
      this.staffLocationService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
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
