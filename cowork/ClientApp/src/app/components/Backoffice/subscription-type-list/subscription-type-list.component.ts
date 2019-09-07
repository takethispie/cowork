import {Component, OnInit} from '@angular/core';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-subscription-type-list',
  templateUrl: './subscription-type-list.component.html',
  styleUrls: ['./subscription-type-list.component.scss'],
})
export class SubscriptionTypeListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<SubscriptionType>;

  constructor(private subscriptionTypeService: SubscriptionTypeService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "Name", "Nom", ""),
      new Field(FieldType.Text, "Description", "Description", ""),
      new Field(FieldType.Number, "FixedContractDurationMonth", "Durée Contrat avec engagement", -1),
      new Field(FieldType.Number, "MonthlyFeeContractFree", "Coût mensuel abonnement contrat sans engagement", -1),
      new Field(FieldType.Number, "MonthlyFeeFixedContract", "Coût mensuel abonnement contrat avec engagement", -1),
      new Field(FieldType.Number, "PriceDay", "Prix journée (5heures et plus)", -1),
      new Field(FieldType.Number, "PriceDayStudent", "Prix journée étudiant", -1),
      new Field(FieldType.Number, "PriceFirstHour", "Prix première heure", -1),
      new Field(FieldType.Number, "PriceNextHalfHour", "Prix demi-heure suivante", -1),
    ];
    this.dataHandler = new TableDataHandler<SubscriptionType>(this.subscriptionTypeService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }
  

  ngOnInit() {
    this.dataHandler.Refresh();
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new SubscriptionType();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Name = fieldDic["Name"][0].Value as string;
    model.Description = fieldDic["Description"][0].Value as string;
    model.FixedContractDurationMonth = fieldDic["FixedContractDurationMonth"][0].Value as number;
    model.MonthlyFeeContractFree = fieldDic["MonthlyFeeContractFree"][0].Value as number;
    model.MonthlyFeeFixedContract = fieldDic["MonthlyFeeFixedContract"][0].Value as number;
    model.PriceDay = fieldDic["PriceDay"][0].Value as number;
    model.PriceDayStudent = fieldDic["PriceDayStudent"][0].Value as number;
    model.PriceFirstHour = fieldDic["PriceFirstHour"][0].Value as number;
    model.PriceNextHalfHour = fieldDic["PriceNextHalfHour"][0].Value as number;
    return model;
  }
}
