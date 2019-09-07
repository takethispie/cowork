import {Component, OnInit} from '@angular/core';
import {Subscription} from '../../../models/Subscription';
import {SubscriptionService} from '../../../services/subscription.service';
import {ModalController} from '@ionic/angular';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DateTime} from 'luxon';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';
import {PlaceService} from '../../../services/place.service';
import {flatMap, map} from 'rxjs/operators';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss'],
})
export class SubscriptionListComponent implements OnInit {
    fields: Field[];
  private dataHandler: TableDataHandler<Subscription>;

  constructor(private  subscriptionService: SubscriptionService, public modalCtrl: ModalController, 
              public subTypeService: SubscriptionTypeService, public placeService: PlaceService) {
    this.placeService.All().pipe(
      flatMap(places => this.subTypeService.All().pipe(map(subTypes => ({ Places: places, Types: subTypes }) )))  
    ).subscribe({
      next: value => {
        const placeOptions = value.Places.map(place => ({ Label: place.Name, Value: place.Id }) );
        const typeOptions = value.Types.map(type => ({ Label: type.Name, Value: type.Id }) );
        this.fields = [
          new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
          new Field(FieldType.Number, "UserId", "Id de l'utilisateur",-1),
          new Field(FieldType.CheckBox, "FixedContract", "Contrat avec engagement", false),
          new Field(FieldType.Select, "PlaceId", "Espace de coworking", 0, placeOptions),
          new Field(FieldType.Select, "TypeId", "Type d'abonnement", 0, typeOptions)
        ];
        this.dataHandler = new TableDataHandler<Subscription>(this.subscriptionService, this.modalCtrl, this.fields, this.CreateModelFromFields);
      }
    });
    
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Subscription();
    model.Id = fieldDic["Id"][0].Value as number;
    model.ClientId = fieldDic["UserId"][0].Value as number;
    model.FixedContract = fieldDic["FixedContract"][0].Value as boolean;
    model.LatestRenewal = DateTime.local();
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.TypeId = fieldDic["TypeId"][0].Value as number;
    return model;
  }
}
