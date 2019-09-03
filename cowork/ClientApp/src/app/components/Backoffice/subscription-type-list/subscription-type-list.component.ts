import { Component, OnInit } from '@angular/core';
import {SubscriptionType} from '../../../models/SubscriptionType';
import {SubscriptionTypeService} from '../../../services/subscription-type.service';

@Component({
  selector: 'app-subscription-type-list',
  templateUrl: './subscription-type-list.component.html',
  styleUrls: ['./subscription-type-list.component.scss'],
})
export class SubscriptionTypeListComponent implements OnInit {

  data: SubscriptionType[];

  constructor(private subscriptionTypeService: SubscriptionTypeService) { }

  ngOnInit() {
    this.subscriptionTypeService.All().subscribe(res => this.data = res);
  }

}
