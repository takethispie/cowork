import {Component, OnInit} from '@angular/core';
import {Place} from '../../../models/Place';
import {PlaceService} from '../../../services/place.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.scss'],
})
export class PlaceListComponent implements OnInit {
  data: Place[];
  fields: Field[];
  private page: number;
  private amount: number;

  constructor(private placeService: PlaceService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "Name", "Nom", ""),
      new Field(FieldType.Number, "CosyRoomAmount", "Nombre de salons cosy", 0),
      new Field(FieldType.CheckBox, "HighBandwidthWifi", "Wifi très haut débit", false),
      new Field(FieldType.Number, "LaptopAmount", "Nombre d'ordinateurs portables", 0),
      new Field(FieldType.CheckBox, "MembersOnlyArea", "Espace réservé aux membres", false),
      new Field(FieldType.CheckBox, "UnlimitedBeverages", "Snack & boissons illimitées", false),
      new Field(FieldType.Number, "PrinterAmount", "Nombre d'imprimantes", 0)
    ];
  }

  ngOnInit() {
    this.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Place();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Name = fieldDic["Name"][0].Value as string;
    model.CosyRoomAmount = fieldDic["CosyRoomAmount"][0].Value as number;
    model.HighBandwidthWifi = fieldDic["HighBandwidthWifi"][0].Value as boolean;
    model.LaptopAmount = fieldDic["LaptopAmount"][0].Value as number;
    model.MembersOnlyArea = fieldDic["MembersOnlyArea"][0].Value as boolean;
    model.UnlimitedBeverages = fieldDic["UnlimitedBeverages"][0].Value as boolean;
    model.PrinterAmount = fieldDic["PrinterAmount"][0].Value as number;
    return model;
  }

  async UpdateItem(model: Place, fields: Field[]) {
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
      mode === "Update" ? this.placeService.Update(user).subscribe(observer) : this.placeService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.placeService.DeleteById(id).subscribe({
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
    this.placeService.AllWithPaging(this.page, this.amount).subscribe({
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
