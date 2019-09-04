import { Component, OnInit } from '@angular/core';
import {Place} from '../../../models/Place';
import {PlaceService} from '../../../services/place.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {Meal} from "../../../models/Meal";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.scss'],
})
export class PlaceListComponent implements OnInit {
  data: Place[];
  fields: Field[];

  constructor(private placeService: PlaceService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "Name", Label: "Nom", Value: ''},
      { Type: "Text", Name: "CosyRoomAmount", Label: "Nombre de salons cosy", Value: 0},
      { Type: "Checkbox", Name: "HighBandwidthWifi", Label: "Wifi très haut débit", Value: false},
      { Type: "Text", Name: "LaptopAmount", Label: "Nombre d'ordinateurs portables", Value: 0},
      { Type: "Checkbox", Name: "MembersOnlyArea", Label: "Espace membres", Value: false},
      { Type: "Checkbox", Name: "UnlimitedBeverages", Label: "Snack & boissons illimitées", Value: false},
      { Type: "Text", Name: "PrinterAmount", Label: "Nombre d'imprimantes", Value: 0}
    ];
  }

  ngOnInit() {
    this.placeService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Place();
    model.Id = -1;
    model.Name = fieldDic["Name"][0].Value as string;
    model.CosyRoomAmount = fieldDic["CosyRoomAmount"][0].Value as number;
    model.HighBandwidthWifi = fieldDic["HighBandwidthWifi"][0].Value as boolean;
    model.LaptopAmount = fieldDic["LaptopAmount"][0].Value as number;
    model.MembersOnlyArea = fieldDic["MembersOnlyArea"][0].Value as boolean;
    model.UnlimitedBeverages = fieldDic["UnlimitedBeverages"][0].Value as boolean;
    model.PrinterAmount = fieldDic["PrinterAmount"][0].Value as number;
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
      this.placeService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.placeService.DeleteById(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
