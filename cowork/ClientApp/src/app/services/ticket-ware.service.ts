import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TicketWare} from '../models/TicketWare';
import {CONTENTJSON} from '../Utils';

@Injectable({
  providedIn: 'root'
})
export class TicketWareService {

  constructor(public http: HttpClient) { }

  All() {
    return this.http.get<TicketWare[]>("api/Ticket/AllTicketWare");
  }


  AllWithPaging(page: number, amount: number) {
    return this.http.get<TicketWare[]>("api/Ticket/WareWithPaging/" + page + "/" + amount);
  }


  Create(ticketWare: TicketWare) {
    return this.http.post<number>("api/Ticket/Ware", ticketWare, CONTENTJSON);
  }


  Update(ticketWare: TicketWare) {
    return this.http.put<number>("api/Ticket/Ware", ticketWare, CONTENTJSON);
  }


  Delete(id: number) {
    return this.http.delete("api/Ticket/Ware/" + id);
  }
}
