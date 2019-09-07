import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TicketComment} from "../models/TicketComment";
import {CONTENTJSON} from "../Utils";

@Injectable({
  providedIn: 'root'
})
export class TicketCommentService {

  constructor(public http: HttpClient) { }

  public Create(ticketComment: TicketComment) {
    return this.http.post<number>("api/Ticket/AddComment", ticketComment, CONTENTJSON);
  }
  

  public Delete(commentId: number) {
    return this.http.delete("api/Ticket/Comment/" + commentId);
  }

  
  public All() {
    return this.http.get<TicketComment[]>("api/Ticket/AllComments");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<TicketComment[]>("api/Ticket/CommentsWithPaging/" + page + "/" + amount);
  }
  
  
  public Update(comment: TicketComment) {
    return this.http.put<number>("api/Ticket/Comment", comment, CONTENTJSON);
  }
}
