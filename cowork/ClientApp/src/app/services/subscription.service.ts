import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {Subscription} from "../models/Subscription";
import {DateTime} from 'luxon';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeArray(subs: Subscription[]) {
    return subs.map(SubscriptionService.ParseDateTime);
  }

  private static ParseDateTime(sub: Subscription) {
    sub.LatestRenewal = DateTime.fromISO(sub.LatestRenewal as unknown as string, { zone: "utc" });
    return sub;
  }

  public All() {
    return this.http.get<Subscription[]>("api/Subscription").pipe(map(this.ParseDateTimeArray));
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<Subscription[]>("api/Subscription/WithPaging/" + page + "/" + amount);
  }


  public Create(sub: Subscription) {
    return this.http.post<number>("api/Subscription", sub, CONTENTJSON);
  }


  public Update(sub: Subscription) {
    return this.http.put<number>("api/Subscription", sub, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Subscription/" + id);
  }


  public ById(id: number) {
    return this.http.get<Subscription>("api/Subscription/ById/" + id).pipe(map(SubscriptionService.ParseDateTime));
  }
  
  
  public OfUser(userId: number) {
    return this.http.get<Subscription>("api/Subscription/OfUser/" + userId).pipe(map(SubscriptionService.ParseDateTime));
  }
}
