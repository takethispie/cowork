import {Component, OnInit} from '@angular/core';
import {Place} from '../../../models/Place';
import {PlaceService} from '../../../services/place.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.scss'],
})
export class PlaceListComponent implements OnInit {
  fields: Field[];
  dataHandler: TableDataHandler<Place>;

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
    this.dataHandler = new TableDataHandler<Place>(this.placeService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
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
}
