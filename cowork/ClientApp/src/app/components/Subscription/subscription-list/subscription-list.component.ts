import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SubscriptionType} from '../../../models/SubscriptionType';

@Component({
  selector: 'subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss'],
})
export class SubscriptionListComponent implements OnInit {
  @Input() SubscriptionTypeList: SubscriptionType[];
  @Input() SelectedId: number;
  @Output() ChosenSubscriptionType: EventEmitter<SubscriptionType> = new EventEmitter();

  constructor() { }

  ngOnInit() {}

  Choose(item: SubscriptionType) {
    if(this.SelectedId === item.Id) this.ChosenSubscriptionType.emit(null);
    else this.ChosenSubscriptionType.emit(item);
  }
}
