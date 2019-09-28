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
    return this.http.get<TicketWare[]>("api/TicketWare");
  }


  AllWithPaging(page: number, amount: number) {
    return this.http.get<TicketWare[]>("api/TicketWare/WithPaging/" + page + "/" + amount);
  }


  Create(ticketWare: TicketWare) {
    return this.http.post<number>("api/TicketWare", ticketWare, CONTENTJSON);
  }


  Update(ticketWare: TicketWare) {
    return this.http.put<number>("api/TicketWare", ticketWare, CONTENTJSON);
  }


  Delete(id: number) {
    return this.http.delete("api/TicketWare/" + id);
  }
}
