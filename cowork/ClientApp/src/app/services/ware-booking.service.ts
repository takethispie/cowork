import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {WareBooking} from '../models/WareBooking';
import {DateTime} from 'luxon';
import {CONTENTJSON} from '../Utils';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WareBookingService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeInArray = (bookings: WareBooking[]) => {
    return bookings.map(this.ParseDateTime);
  };

  private ParseDateTime = booking => {
    booking.Start = DateTime.fromISO(booking.Start as unknown as string);
    booking.End = DateTime.fromISO(booking.End as unknown as string);
    return booking;
  };

  public All() {
    return this.http.get<WareBooking[]>("api/WareBooking")
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllByWareId(wareId: number) {
    return this.http.get<WareBooking[]>("api/WareBooking/ByWareId/" + wareId)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllByWareIdStartingAt(wareId: number, date: DateTime) {
    return this.http.post<WareBooking[]>("api/WareBooking/ByWareIdStartingAt/" + wareId, date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllOfUser(userId: number): Observable<WareBooking[]> {
    return this.http.get<WareBooking[]>("api/WareBooking").pipe(map(bookings => this.ParseDateTimeInArray(bookings)));;
  }

  public ById(id: number) {
    return this.http.get<WareBooking>("api/WareBooking/" + id).pipe(map(this.ParseDateTime));
  }

  public AllStartingAt(date: DateTime) {
    return this.http.post<WareBooking[]>("api/WareBooking/AllStartingAt", date, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllWithPagingStartingAt(page: number, size: number, dateTime: DateTime) {
    return this.http.post<WareBooking[]>("api/WareBooking/WithPaging/" + page + "/" + size, dateTime, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public AllWithPaging(page: number, size: number) {
    return this.http.get<WareBooking[]>("api/WareBooking/WithPaging/" + page + "/" + size)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }

  public Update(wareBooking: WareBooking) {
    return this.http.put<number>("api/WareBooking", wareBooking, CONTENTJSON);
  }

  public Delete(id: number) {
    return this.http.delete("api/WareBooking/" + id);
  }

  public Create(wareBooking: WareBooking) {
    return this.http.post<number>("api/WareBooking", wareBooking, CONTENTJSON);
  }

  public AllFromGivenDate(dateTime: DateTime) {
    return this.http.post<WareBooking[]>("api/WareBooking/FromGivenDate", dateTime, CONTENTJSON)
        .pipe(map(bookings => this.ParseDateTimeInArray(bookings)));
  }
}
