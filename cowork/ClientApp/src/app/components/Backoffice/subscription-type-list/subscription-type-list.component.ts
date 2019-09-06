import { Component, OnInit } from '@angular/core';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {Subscription} from "../../../models/Subscription";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {MealBooking} from "../../../models/MealBooking";

@Component({
  selector: 'app-subscription-type-list',
  templateUrl: './subscription-type-list.component.html',
  styleUrls: ['./subscription-type-list.component.scss'],
})
export class SubscriptionTypeListComponent implements OnInit {

  data: SubscriptionType[];
  fields: Field[];

  constructor(private subscriptionTypeService: SubscriptionTypeService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: -1},
      { Type: "Text", Name: "Name", Label: "Nom", Value: null},
      { Type: "Text", Name: "Description", Label: "Description", Value: null },
      { Type: "Number", Name: "FixedContractDurationMonth", Label: "Durée Contrat avec engagement", Value: null},
      { Type: "Number", Name: "MonthlyFeeContractFree", Label: "Coût mensuel abonnement contrat sans engagement", Value: null},
      { Type: "Number", Name: "MonthlyFeeFixedContract", Label: "Coût mensuel abonnement contrat avec engagement", Value: null},
      { Type: "Number", Name: "PriceDay", Label: "Prix journée (5heures et plus)", Value: null},
      { Type: "Number", Name: "PriceDayStudent", Label: "Prix journée étudiant", Value: null},
      { Type: "Number", Name: "PriceFirstHour", Label: "Prix première heure", Value: null},
      { Type: "Number", Name: "PriceNextHalfHour", Label: "Prix demi-heure suivante", Value: null}
    ]
  }
    
  

  ngOnInit() {
    this.subscriptionTypeService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new SubscriptionType();
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

  async UpdateItem(model: SubscriptionType, fields: Field[]) {
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
      mode === "Update" ? this.subscriptionTypeService.Update(user).subscribe(observer) : this.subscriptionTypeService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.subscriptionTypeService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
