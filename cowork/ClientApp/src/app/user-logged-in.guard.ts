import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable, of} from 'rxjs';
import {AuthService} from './services/auth.service';
import {UserService} from './services/user.service';
import {map} from 'rxjs/operators';
import * as jwt_decode from 'jwt-decode';

@Injectable({
    providedIn: 'root'
})
export class UserLoggedInGuard implements CanActivate {
    constructor(public auth: AuthService, public router: Router, public userService: UserService) { }


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        const auth = localStorage.getItem('auth_token');
        const userId = localStorage.getItem('UserId');
        const placeId = localStorage.getItem('PlaceId');
        const userType = localStorage.getItem('UserType');
        if(auth === "null" || userId === "null" || userType === "null") {
            this.router.navigate(["Auth"]);
        } else {
            if(this.isTokenExpired(auth)) this.router.navigate(["Auth"]);
            this.auth.UserId = parseInt(userId, 10);
            this.auth.PlaceId = parseInt(placeId, 10);
            this.auth.UserType = parseInt(userType, 10);
            if(this.auth.User == null) {
                return this.userService.ById(parseInt(userId, 10)).pipe(
                    map(res => {
                        this.auth.User = res;
                        return true;
                    })
                );
            } else return of(true);
        }
        return of(false);
    }


    getTokenExpirationDate(token: string): Date {
        const decoded = jwt_decode(token);
        if (decoded.exp === undefined) return null;
        const date = new Date(0);
        date.setUTCSeconds(decoded.exp);
        return date;
    }

    isTokenExpired(token?: string): boolean {
        if(!token) return true;
        const date = this.getTokenExpirationDate(token);
        if(date === undefined) return false;
        return !(date.valueOf() > new Date().valueOf());
    }
}

