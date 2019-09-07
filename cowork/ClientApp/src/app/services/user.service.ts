import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {User} from "../models/User";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(public http: HttpClient) { }

  public All() {
    return this.http.get<User[]>("api/User/ALL");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<User[]>("api/User/WithPaging/" + page + "/" + amount);
  }
  
  public Create(user: User) {
    return this.http.post<number>("api/User", user, CONTENTJSON);
  }


  public Update(ticket: User) {
    return this.http.put<number>("api/User", ticket, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/User/" + id);
  }


  public ById(id: number) {
    return this.http.get<User>("api/User/" + id);
  }
  
  
  public UserWithEmail(email: string) {
    return this.http.get<User>("api/User/WithEmail/" + email);
  }
}
