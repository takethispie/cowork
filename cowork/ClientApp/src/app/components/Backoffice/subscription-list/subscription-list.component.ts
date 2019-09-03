import { Component, OnInit } from '@angular/core';
import {Subscription} from '../../../models/Subscription';
import {SubscriptionService} from '../../../services/subscription.service';

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss'],
})
export class SubscriptionListComponent implements OnInit {
    data: Subscription[];

  constructor(private  subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.subscriptionService.All().subscribe(res => this.data = res);
  }

}
