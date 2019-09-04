import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {Ticket} from "../models/Ticket";
import {DateTime} from 'luxon';
import {map} from 'rxjs/operators';
import {TicketComment} from '../models/TicketComment';
import {tick} from '@angular/core/testing';
import {TicketAttribution} from '../models/TicketAttribution';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(public http: HttpClient) { }

  private ParseDateTimeArray(tickets: Ticket[]) {
    return tickets.map(ticket => {
      ticket.Created = TicketService.ParseDateTime(ticket.Created);
      return ticket;
    });
  }

  private static ParseDateTime(prop: DateTime) {
    prop = DateTime.fromISO(prop as unknown as string);
    return prop;
  }

  public All() {
    return this.http.get<Ticket[]>("api/Ticket").pipe(map(this.ParseDateTimeArray));;
  }


  public Create(ticket: Ticket) {
    return this.http.post<number>("api/Ticket", ticket, CONTENTJSON);
  }
  
  public CreateAttribution(ticketAttribution: TicketAttribution) {
    return this.http.post<number>("api/Ticket/Attribution", ticketAttribution, CONTENTJSON);
  }
  
  public DeleteAttribution(id: number) {
    return this.http.delete("api/Ticket/Attribution/" + id);
  }


  public Update(ticket: Ticket) {
    return this.http.put<number>("api/Ticket", ticket, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Ticket/" + id);
  }


  public ById(id: number) {
    return this.http.get<Ticket>("api/Ticket/" + id).pipe(map(ticket => {
      ticket.Created = TicketService.ParseDateTime(ticket.Created);
      return ticket;
    }));
  }
  
  
  public OpenedBy(userId: number) {
    return this.http.get<Ticket[]>("api/Ticket/OpenedBy/" + userId).pipe(map(this.ParseDateTimeArray));
  }


  public AllAttribution() {
    return this.http.get<TicketAttribution[]>("api/Ticket/AllAttributions");
  }

  public AllComments() {
    return this.http.get<TicketComment[]>("api/Ticket/AllComments");
  }
  
  
  public AttributedTo(personnalId: number) {
    return this.http.get<Ticket[]>("api/Ticket/AttributedTo/" + personnalId).pipe(map(this.ParseDateTimeArray));
  }

  public AddComment(ticketComment: TicketComment) {
    return this.http.post<number>("api/Ticket/AddComment", ticketComment, CONTENTJSON);
  }

  public DeleteComment(commentId: number) {
    return this.http.delete("api/Ticket/Comment/" + commentId);
  }
}
