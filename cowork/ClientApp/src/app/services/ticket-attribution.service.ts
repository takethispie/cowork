import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TicketAttribution} from "../models/TicketAttribution";
import {CONTENTJSON} from "../Utils";

@Injectable({
  providedIn: 'root'
})
export class TicketAttributionService {

  constructor(public http: HttpClient) { }

  public Create(ticketAttribution: TicketAttribution) {
    return this.http.post<number>("api/TicketAttribution", ticketAttribution, CONTENTJSON);
  }
  

  public Delete(id: number) {
    return this.http.delete("api/TicketAttribution/" + id);
  }
  

  public All() {
    return this.http.get<TicketAttribution[]>("api/TicketAttribution");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<TicketAttribution[]>("api/TicketAttribution/WithPaging/" + page + "/" + amount);
  }
  
  
  public Update(attribution: TicketAttribution) {
    return this.http.put<number>("api/TicketAttribution", attribution, CONTENTJSON);
  }
}
