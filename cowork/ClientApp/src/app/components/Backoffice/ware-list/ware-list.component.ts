import {Component, OnInit} from '@angular/core';
import {Ware} from '../../../models/Ware';
import {WareService} from '../../../services/ware.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {ModalController} from '@ionic/angular';
import List from 'linqts/dist/src/list';
import {WareBooking} from '../../../models/WareBooking';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ware-list',
  templateUrl: './ware-list.component.html',
  styleUrls: ['./ware-list.component.scss'],
})
export class WareListComponent implements OnInit {

  data: Ware[];
  fields: Field[];
  dataHandler: TableDataHandler<Ware>;

  constructor(private wareService: WareService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "Name", "Nom", ""),
      new Field(FieldType.Text, "Description", "Description", ""),
      new Field(FieldType.Number, "PlaceId", "Id espace de coworking", -1),
      new Field(FieldType.Text, "SerialNumber", "Numéro de série", ""),
      new Field(FieldType.CheckBox, "InStorage", "En réserve", false)
    ];
    this.dataHandler = new TableDataHandler<Ware>(this.wareService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.wareService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Ware();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Name = fieldDic["Name"][0].Value as string;
    model.Description = fieldDic["Description"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.SerialNumber = fieldDic["SerialNumber"][0].Value as string;
    model.InStorage = fieldDic["InStorage"][0].Value as boolean;
    return model;
  }
}
