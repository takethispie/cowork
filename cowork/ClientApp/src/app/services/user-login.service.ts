import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {of} from 'rxjs';
import {Login} from '../models/Login';
import {CONTENTJSON} from '../Utils';

@Injectable({
  providedIn: 'root'
})
export class UserLoginService {

  constructor(public http: HttpClient) { }

  public Create(login: Login) {
    return this.http.post<number>("api/Login", login, CONTENTJSON);
  }


  public Update(login: Login) {
    return this.http.put<number>("api/Login", login, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Login/" + id);
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<Login[]>("api/Login/WithPaging/" + page + "/" + amount);
  }
}
