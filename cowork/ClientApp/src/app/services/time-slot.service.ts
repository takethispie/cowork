import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {TimeSlot} from "../models/TimeSlot";
import {DateTime} from 'luxon';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TimeSlotService {

  constructor(public http: HttpClient) { }

  private TimeSlotToUtc(timeslot: TimeSlot) {
    let start = DateTime.local().set({hour: timeslot.StartHour, minute: timeslot.StartMinutes});
    let end = DateTime.local().set({hour: timeslot.EndHour, minute: timeslot.EndMinutes});
    start = start.toUTC();
    end = end.toUTC();
    timeslot.StartHour = start.hour;
    timeslot.StartMinutes = start.minute;
    timeslot.EndHour = end.hour;
    timeslot.EndMinutes = end.minute;
  }


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
    return this.http.get<TimeSlot[]>("api/TimeSlot").pipe(
        map((timeslots: TimeSlot[]) => map((ts: TimeSlot) => this.TimeSlotFromUtc(ts)))
    );
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<TimeSlot[]>("api/TimeSlot/WithPaging/" + page + "/" + amount).pipe(
        map((timeslots: TimeSlot[]) => {
          return timeslots.map(ts => this.TimeSlotFromUtc(ts))
        })
    );
  }


  public AllFromPlace(placeId: number) {
    return this.http.get<TimeSlot[]>("api/TimeSlot/OfPlace/" + placeId).pipe(
        map((timeslots: TimeSlot[]) => {
          return timeslots.map(ts => this.TimeSlotFromUtc(ts))
        })
    );
  }

  public Create(timeslot: TimeSlot) {
    this.TimeSlotToUtc(timeslot);
    return this.http.post<number>("api/TimeSlot", timeslot, CONTENTJSON);
  }


  public Update(timeslot: TimeSlot) {
    this.TimeSlotToUtc(timeslot);
    return this.http.put<number>("api/TimeSlot", timeslot, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/TimeSlot/" + id);
  }


  public ById(id: number) {
    return this.http.get<TimeSlot>("api/TimeSlot/" + id).pipe(
        map(ts => this.TimeSlotFromUtc(ts))
    );
  }
}

