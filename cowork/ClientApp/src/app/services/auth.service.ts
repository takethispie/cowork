import { Injectable } from '@angular/core';
import {Observable, of} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {User} from '../models/User';
import {map} from 'rxjs/operators';
import {Subscription} from '../models/Subscription';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    public User: User;
    public Subscription: Subscription;

    constructor(public http: HttpClient) {
        this.User = null;
    }

    public Login(email: string, password: string): Observable<{user: User, sub: Subscription}> {
        return this.http.post<{user: User, sub: Subscription}>("api/user/auth", { Email: email, Password: password}, CONTENTJSON).pipe(
            map(res => {
             if(res != null) {
                 this.User = res.user;
                 this.Subscription = res.sub;
             }
             return res;
            })
        );
    }


    public Register(user: User, password: string, email: string): Observable<number> {
        return this.http.post<number>("api/user/register",{ User: user, Password: password, Email: email }, CONTENTJSON);
    }
}
