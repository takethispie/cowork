import { Component, OnInit } from '@angular/core';
import {Subscription} from '../../../models/Subscription';
import {SubscriptionService} from '../../../services/subscription.service';
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {StaffLocation} from "../../../models/StaffLocation";
import {DateTime} from "luxon";
import {SubscriptionTypeService} from "../../../services/subscription-type.service";
import {PlaceService} from "../../../services/place.service";
import {flatMap, map} from "rxjs/operators";
import {MealBooking} from "../../../models/MealBooking";

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss'],
})
export class SubscriptionListComponent implements OnInit {
    data: Subscription[];
    fields: Field[];

  constructor(private  subscriptionService: SubscriptionService, public modalCtrl: ModalController, 
              public subTypeService: SubscriptionTypeService, public placeService: PlaceService) {
    this.placeService.All().pipe(
      flatMap(places => this.subTypeService.All().pipe(map(subTypes => ({ Places: places, Types: subTypes }) )))  
    ).subscribe({
      next: value => {
        const placeOptions = value.Places.map(place => ({ Label: place.Name, Value: place.Id }) );
        const typeOptions = value.Types.map(type => ({ Label: type.Name, Value: type.Id }) );
        this.fields = [
          { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: "-1"},
          { Type: "Text", Name: "UserId", Label: "Id de l'utilisateur", Value: null},
          { Type: "Checkbox", Name: "FixedContract", Label: "Contrat avec engagement", Value: false},
          { Type: "Select", Name: "PlaceId", Label: "Id de l'espace de coworking", Value: "0" , Options: placeOptions },
          { Type: "Select", Name: "TypeId", Label: "Id du type d'abonnement", Value: "0", Options: typeOptions}
        ];
      }
    });
    
  }

  ngOnInit() {
    this.subscriptionService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Subscription();
    model.Id = fieldDic["Id"][0].Value as number;
    model.ClientId = fieldDic["UserId"][0].Value as number;
    model.FixedContract = fieldDic["FixedContract"][0].Value as boolean;
    model.LatestRenewal = DateTime.local();
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.TypeId = fieldDic["TypeId"][0].Value as number;
    return model;
  }

  async UpdateItem(model: Subscription, fields: Field[]) {
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
      mode === "Update" ? this.subscriptionService.Update(user).subscribe(observer) : this.subscriptionService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.subscriptionService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }
}
