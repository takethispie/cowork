import { Component, OnInit } from '@angular/core';
import {Ware} from '../../../models/Ware';
import {WareService} from '../../../services/ware.service';
import {Field} from "../../dynamic-form-builder/Field";
import {ModalController} from "@ionic/angular";
import List from "linqts/dist/src/list";
import {WareBooking} from "../../../models/WareBooking";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";

@Component({
  selector: 'app-ware-list',
  templateUrl: './ware-list.component.html',
  styleUrls: ['./ware-list.component.scss'],
})
export class WareListComponent implements OnInit {

  data: Ware[];
  fields: Field[];

  constructor(private wareService: WareService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "Name", Label: "Nom", Value: null},
      { Type: "Text", Name: "Description", Label: "Description", Value: null},
      { Type: "Text", Name: "PlaceId", Label: "Id de l'espace de coworking", Value: null},
      { Type: "Text", Name: "SerialNumber", Label: "Numéro de série", Value: null},
      { Type: "Checkbox", Name: "InStorage", Label: "En réserve", Value: null}
    ]
    
  }

  ngOnInit() {
    this.wareService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Ware();
    model.Id = -1;
    model.Name = fieldDic["Name"][0].Value as string;
    model.Description = fieldDic["Description"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.SerialNumber = fieldDic["SerialNumber"][0].Value as string;
    model.InStorage = fieldDic["InStorage"][0].Value as boolean;
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
      this.wareService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.wareService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
