import { Component, OnInit } from '@angular/core';
import {MealBooking} from '../../../models/MealBooking';
import {MealBookingService} from '../../../services/meal-booking.service';

@Component({
  selector: 'app-meal-booking-list',
  templateUrl: './meal-booking-list.component.html',
  styleUrls: ['./meal-booking-list.component.scss'],
})
export class MealBookingListComponent implements OnInit {
    data: MealBooking[];

  constructor(private mealBookingService: MealBookingService) { }

  ngOnInit() {
    this.mealBookingService.All().subscribe(res => this.data = res);
  }

}
