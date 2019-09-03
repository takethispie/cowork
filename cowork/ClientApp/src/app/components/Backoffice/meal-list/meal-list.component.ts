import { Component, OnInit } from '@angular/core';
import {Meal} from '../../../models/Meal';
import {MealService} from '../../../services/meal.service';

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.scss'],
})
export class MealListComponent implements OnInit {
    data: Meal[];

  constructor(private mealService: MealService) { }

  ngOnInit() {
    this.mealService.All().subscribe(res => this.data = res);
  }

}
