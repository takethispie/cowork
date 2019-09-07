import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {SubscriptionType} from "../models/SubscriptionType";

@Injectable({
  providedIn: 'root'
})
export class SubscriptionTypeService {

  constructor(public http: HttpClient) { }

  public All() {
    return this.http.get<SubscriptionType[]>("api/SubscriptionType");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<SubscriptionType[]>("api/SubscriptionType/WithPaging/" + page + "/" + amount);
  }


  public Create(sub: SubscriptionType) {
    return this.http.post<number>("api/SubscriptionType", sub, CONTENTJSON);
  }


  public Update(sub: SubscriptionType) {
    return this.http.put<number>("api/SubscriptionType", sub, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/SubscriptionType/" + id);
  }


  public ById(id: number) {
    return this.http.get<SubscriptionType>("api/SubscriptionType/ById/" + id);
  }
  

  public ByName(name: string) {
    return this.http.get<SubscriptionType>("api/SubscriptionType/ByName/" + name);
  }
}
