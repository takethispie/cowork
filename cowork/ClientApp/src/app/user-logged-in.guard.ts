import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthService} from './services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class UserLoggedInGuard implements CanActivate {
    constructor(public auth: AuthService, public router: Router) { }


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if(this.auth.User == null) {
            this.router.navigate(["Auth"]);
        } else return true;
        return false;
    }

}

