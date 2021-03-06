import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RoomBooking} from "../models/RoomBooking";
import {BookingToUtc, CONTENTJSON} from '../Utils';
import {DateTime} from "luxon";
import {map} from 'rxjs/operators';
import {MealBooking} from '../models/MealBooking';
import {of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoomBookingService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeInArray = (bookings: RoomBooking[]) => {
    return bookings.map(this.ParseDateTime);
  };

  private ParseDateTime = booking => {
    booking.Start = DateTime.fromISO(booking.Start as unknown as string, { zone: "utc" });
    booking.End = DateTime.fromISO(booking.End as unknown as string, { zone: "utc" });
    return booking;
  };
  
  public All() {
    return this.http.get<RoomBooking[]>("api/RoomBooking")
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }


  public Create(roomBooking: RoomBooking) {
    if(roomBooking.Start.day !== roomBooking.End.day) return of(-1);
    roomBooking = BookingToUtc<RoomBooking>(roomBooking);
    return this.http.post<number>("api/RoomBooking", roomBooking, CONTENTJSON);
  }


  public Update(roomBooking: RoomBooking) {
    roomBooking = BookingToUtc<RoomBooking>(roomBooking);
    return this.http.put<number>("api/RoomBooking", roomBooking, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/RoomBooking/" + id);
  }


  public ById(id: number) {
    return this.http.get<RoomBooking>("api/RoomBooking/" + id).pipe(map(this.ParseDateTime));
  }


  public AllOfUser(id: number) {
    return this.http.get<RoomBooking[]>("api/RoomBooking/OfUser/" + id)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }


  public AllOfRoom(id: number) {
    return this.http.get<RoomBooking[]>("api/RoomBooking/OfRoom/" + id)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }


  public AllFromGivenDate(date: DateTime) {
    return this.http.post<RoomBooking[]>("api/RoomBooking/FromGivenDate", date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllOfRoomStartingAtDate(roomId: number, date: DateTime) {
    return this.http.post<RoomBooking[]>("api/RoomBooking/GetAllOfRoomStartingAtDate/" + roomId, date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }


  public AllWithPaging(page: number, amount: number) {
      return this.http.get<RoomBooking[]>("api/RoomBooking/WithPaging/" + page + "/" + amount);
  }
}
