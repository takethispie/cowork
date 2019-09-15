import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TicketComment} from "../models/TicketComment";
import {CONTENTJSON, ParseDateTime} from '../Utils';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TicketCommentService {

  constructor(public http: HttpClient) { }


  private ParseDateTimeArray(comments: TicketComment[]) {
    return comments.map(ticket => {
      ticket.Created = ParseDateTime(ticket.Created).toISO();
      return ticket;
    });
  }


  public Create(ticketComment: TicketComment) {
    return this.http.post<number>("api/Ticket/AddComment", ticketComment, CONTENTJSON);
  }
  

  public Delete(commentId: number) {
    return this.http.delete("api/Ticket/Comment/" + commentId);
  }

  
  public All() {
    return this.http.get<TicketComment[]>("api/Ticket/AllComments").pipe(map(this.ParseDateTimeArray));;
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<TicketComment[]>("api/Ticket/CommentsWithPaging/" + page + "/" + amount).pipe(map(this.ParseDateTimeArray));;
  }
  
  
  public Update(comment: TicketComment) {
    return this.http.put<number>("api/Ticket/Comment", comment, CONTENTJSON);
  }


  public LastCommentsFromUser(userId: number, commentAmount: number) {
    return this.http.get<TicketComment[]>("api/Ticket/LastCommentsFromUser/" + userId + "/" + commentAmount);
  }
}
