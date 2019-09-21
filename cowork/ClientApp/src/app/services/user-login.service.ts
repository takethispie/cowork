import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {of} from 'rxjs';
import {Login} from '../models/Login';

@Injectable({
  providedIn: 'root'
})
export class UserLoginService {

  constructor(public http: HttpClient) { }

  public Create(login: Login) {
    return of(-1);
  }


  public Update(login: Login) {
    return of(-1);
  }


  public Delete(id: number) {
    return of(null);
  }


  public AllWithPaging(page: number, amount: number) {
    return of([]);
  }
}
