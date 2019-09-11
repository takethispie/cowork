import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {MealBooking} from "../models/MealBooking";
import {BookingToUtc, CONTENTJSON} from '../Utils';
import {DateTime} from "luxon";
import {WareBooking} from '../models/WareBooking';

@Injectable({
  providedIn: 'root'
})
export class MealBookingService {

  constructor(public http: HttpClient) { }
  
  public All(): Observable<MealBooking[]> {
    return this.http.get<MealBooking[]>("api/MealBooking/All");
  }
  
  
  public Create(mealRes: MealBooking) {
    return this.http.post<number>("api/MealBooking", mealRes, CONTENTJSON);
  }
  
  
  public Update(mealRes: MealBooking) {
    return this.http.put<number>("api/MealBooking", mealRes, CONTENTJSON);
  }
  
  
  public Delete(id: number) {
    return this.http.delete("api/MealBooking/" + id);
  }
  
  
  public ById(id: number) {
    return this.http.get<MealBooking>("api/MealBooking/" + id);
  }
  
  
  public AllFromUser(userId: number) {
    return this.http.get<MealBooking[]>("api/MealBooking/FromUser/" + userId);
  }
  
  
  public AllFromDateAndPlace(date: DateTime, placeId: number) {
    return this.http.post<MealBooking[]>("api/MealBooking/FromDateAndPlace", { Date: date, PlaceId: placeId }, CONTENTJSON);
  }


  AllWithPaging(page: number, amount: number) {
      return this.http.get<MealBooking[]>("api/MealBooking/WithPaging/" + page + "/" + amount);
  }
}
