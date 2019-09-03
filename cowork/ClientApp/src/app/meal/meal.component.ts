import {Component, OnInit} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {MealService} from '../services/meal.service';
import {Meal} from '../models/Meal';
import {DateTime} from 'luxon';
import {SubscriptionService} from '../services/subscription.service';
import {flatMap} from 'rxjs/operators';
import {of} from 'rxjs';

@Component({
  selector: 'app-tab2',
  templateUrl: 'meal.component.html',
  styleUrls: ['meal.component.scss']
})
export class MealComponent {
  public AvailableMeals: Meal[];

  constructor(public auth: AuthService, public mealService: MealService) {
    this.AvailableMeals = [];
  }

  ionViewWillEnter() {
      return this.mealService.FromPlaceAndStartingAtDate(DateTime.local().plus({ days: 1 }), this.auth.Subscription.Place.Id)
          .subscribe(res => this.AvailableMeals = res);
  }

}
