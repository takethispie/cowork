import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {Place} from "../models/Place";
import { map } from 'rxjs/operators';
import { TimeSlot } from '../models/TimeSlot';
import { DateTime } from 'luxon';

@Injectable({
  providedIn: 'root'
})
export class PlaceService {

  constructor(public http: HttpClient) { }

  public TimeSlotFromUtc(timeslot: TimeSlot) {
    const start = DateTime.utc().set({ hour: timeslot.StartHour, minute: timeslot.StartMinutes, second: 0 ,millisecond: 0}).toLocal();
    const end = DateTime.utc().set({ hour: timeslot.EndHour, minute: timeslot.EndMinutes, second: 0, millisecond: 0}).toLocal();
    timeslot.StartHour = start.hour;
    timeslot.StartMinutes = start.minute;
    timeslot.EndHour = end.hour;
    timeslot.EndMinutes = end.minute;
    return timeslot;
  }


  public All() {
    return this.http.get<Place[]>("api/Place").pipe(
      map(place => {
        return place.map(p => {
          p.OpenedTimes.map(ts => this.TimeSlotFromUtc(ts));
          return p;
        });
      })
    );
  }


  public Create(place: Place) {
    return this.http.post<number>("api/Place", place, CONTENTJSON);
  }


  public Update(place: Place) {
    return this.http.put<number>("api/Place", place, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Place/ById/" + id);
  }


  public DeleteByName(name: string) {
    return this.http.delete("api/Place/ByName/" + name);
  }


  public ById(id: number) {
    return this.http.get<Place>("api/Place/ById/" + id);
  }


  public ByName(name: string) {
    return this.http.get<Place>("api/Place/ByName/" + name);
  }


  public AllWithPaging(page: number, amount: number) {
      return this.http.get<Place[]>("api/Place/WithPaging/" + page + "/" + amount);
  }
}
