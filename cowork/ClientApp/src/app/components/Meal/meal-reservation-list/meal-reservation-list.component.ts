import {Component, Input, OnInit} from '@angular/core';
import {MealBooking} from '../../../models/MealBooking';
import {MealService} from '../../../services/meal.service';

@Component({
  selector: 'meal-reservation-list',
  templateUrl: './meal-reservation-list.component.html',
  styleUrls: ['./meal-reservation-list.component.scss'],
})
export class MealReservationListComponent implements OnInit {
  @Input() MealReservations: MealBooking[];

  constructor(public mealService: MealService) { }

  ngOnInit() {}

}
