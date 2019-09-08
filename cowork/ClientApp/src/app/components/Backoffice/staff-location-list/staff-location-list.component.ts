import {Component, OnInit} from '@angular/core';
import {StaffLocationService} from '../../../services/staff-location.service';
import {StaffLocation} from '../../../models/StaffLocation';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-staff-location-list',
  templateUrl: './staff-location-list.component.html',
  styleUrls: ['./staff-location-list.component.scss'],
})
export class StaffLocationListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<StaffLocation>;

  constructor(private staffLocationService: StaffLocationService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "UserId", "Id utilisateur", -1),
      new Field(FieldType.Number, "PlaceId", "Id espace de coworking", -1)
    ];
    this.dataHandler = new TableDataHandler<StaffLocation>(this.staffLocationService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new StaffLocation();
    model.Id = fieldDic["Id"][0].Value as number;
    model.UserId = fieldDic["UserId"][0].Value as number;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    return model;
  }
}
