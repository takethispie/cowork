import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON, ParseDateTime} from '../Utils';
import {Ticket} from "../models/Ticket";
import {DateTime} from 'luxon';
import {map} from 'rxjs/operators';
import {TicketState} from '../models/TicketState';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeArray(tickets: Ticket[]) {
    return tickets.map(ticket => {
      ticket.Created = ParseDateTime(ticket.Created);
      return ticket;
    });
  }

  public All() {
      return this.http.get<Ticket[]>("api/Ticket").pipe(map(this.ParseDateTimeArray));
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<Ticket[]>("api/Ticket/WithPaging/" + page + "/" + amount).pipe(map(this.ParseDateTimeArray));;
  }


  public Create(ticket: Ticket) {
    return this.http.post<number>("api/Ticket", ticket, CONTENTJSON);
  }


  public Update(ticket: Ticket) {
    return this.http.put<number>("api/Ticket", ticket, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Ticket/" + id);
  }


  public ById(id: number) {
    return this.http.get<Ticket>("api/Ticket/" + id).pipe(map(ticket => {
      ticket.Created = ParseDateTime(ticket.Created);
      return ticket;
    }));
  }
  
  
  public OpenedBy(userId: number) {
    return this.http.get<Ticket[]>("api/Ticket/OpenedBy/" + userId).pipe(map(this.ParseDateTimeArray));
  }
  
  
  public AttributedTo(personnalId: number) {
    return this.http.get<Ticket[]>("api/Ticket/AttributedTo/" + personnalId).pipe(map(this.ParseDateTimeArray));
  }


  public AllWithState(state: TicketState) {
    return this.http.get<Ticket[]>("api/Ticket/WithState/" + state).pipe(map(this.ParseDateTimeArray));;
  }
}
