import { Injectable } from '@angular/core';
import {CONTENTJSON} from "../Utils";
import {Meal} from "../models/Meal";
import {HttpClient} from "@angular/common/http";
import {DateTime} from "luxon";
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeArray(meals: Meal[]) {
    return meals.map(meal => MealService.ParseDateTime(meal));
  }


  private static ParseDateTime(meal: Meal) {
    meal.Date = DateTime.fromISO(meal.Date as unknown as string, { zone: "utc" });
    return meal;
  }


  public All() {
    return this.http.get<Meal[]>("api/meal").pipe(map(this.ParseDateTimeArray));
  }


  public Create(meal: Meal) {
    return this.http.post<number>("api/meal", meal, CONTENTJSON);
  }


  public Update(meal: Meal) {
    return this.http.put<number>("api/meal", meal, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/meal/" + id);
  }


  public ById(id: number) {
    return this.http.get<Meal>("api/meal/" + id).pipe(map(MealService.ParseDateTime));
  }
  
  
  public AllFromPlace(placeId: number) {
    return this.http.get<Meal[]>("api/meal/AllFromPlace/" + placeId).pipe(map(this.ParseDateTimeArray));
  }
  
  
  public FromPlaceAndDate(date: DateTime, placeId: number) {
    return this.http.post<Meal[]>("api/meal/FromPlaceAndDate", { Date: date, PlaceId: placeId }, CONTENTJSON)
        .pipe(map(this.ParseDateTimeArray));
  }


  public FromPlaceAndStartingAtDate(date: DateTime, placeId: number) {
    return this.http.post<Meal[]>("api/meal/FromPlaceAndStartingAtDate", { Date: date, PlaceId: placeId }, CONTENTJSON)
        .pipe(map(this.ParseDateTimeArray));
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<Meal[]>("api/Meal/WithPaging/" + page + "/" + amount);
  }
}
