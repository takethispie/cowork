import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RoomBooking} from "../models/RoomBooking";
import {CONTENTJSON} from "../Utils";
import {DateTime} from "luxon";
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoomBookingService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeInArray = (bookings: RoomBooking[]) => {
    return bookings.map(this.ParseDateTime);
  };

  private ParseDateTime = booking => {
    booking.Start = DateTime.fromISO(booking.Start as unknown as string);
    booking.End = DateTime.fromISO(booking.End as unknown as string);
    return booking;
  };
  
  public All() {
    return this.http.get<RoomBooking[]>("api/RoomBooking")
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }


  public Create(roomBooking: RoomBooking) {
    return this.http.post<number>("api/RoomBooking", roomBooking, CONTENTJSON);
  }


  public Update(roomBooking: RoomBooking) {
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
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));;
  }


  public AllFromGivenDate(date: DateTime) {
    return this.http.post<RoomBooking[]>("api/RoomBooking/FromGivenDate", date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllOfRoomStartingAtDate(roomId: number, date: DateTime) {
    return this.http.post<RoomBooking[]>("api/RoomBooking/GetAllOfRoomStartingAtDate/" + roomId, date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }
}
