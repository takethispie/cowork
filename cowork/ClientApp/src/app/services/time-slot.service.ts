import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {TimeSlot} from "../models/TimeSlot";

@Injectable({
  providedIn: 'root'
})
export class TimeSlotService {

  constructor(public http: HttpClient) { }

  public All() {
    return this.http.get<TimeSlot[]>("api/TimeSlot");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<TimeSlot[]>("api/TimeSlot/WithPaging/" + page + "/" + amount);
  }


  public AllFromPlace(placeId: number) {
    return this.http.get<TimeSlot[]>("api/TimeSlot/OfPlace/" + placeId);
  }

  public Create(timeslot: TimeSlot) {
    return this.http.post<number>("api/TimeSlot", timeslot, CONTENTJSON);
  }


  public Update(timeslot: TimeSlot) {
    return this.http.put<number>("api/TimeSlot", timeslot, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/TimeSlot/" + id);
  }


  public ById(id: number) {
    return this.http.get<TimeSlot>("api/TimeSlot/" + id);
  }
}

