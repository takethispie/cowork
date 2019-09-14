import {Component} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Subscription} from '../../models/Subscription';
import {MealBooking} from '../../models/MealBooking';
import {RoomBooking} from '../../models/RoomBooking';
import {Ticket} from '../../models/Ticket';
import {LoadingService} from '../../services/loading.service';
import {UserType} from '../../models/UserType';
import {Subject} from 'rxjs';

@Component({
    selector: 'app-tab1',
    templateUrl: 'account.component.html',
    styleUrls: ['account.component.scss']
})
export class AccountComponent {

    public userSub: Subscription;
    public userMeals: MealBooking[];
    public roomBookings: RoomBooking[];
    public Tickets: Ticket[];
    public Refresher: Subject<object>;

    constructor(public auth: AuthService, public loading: LoadingService) {
        this.Refresher = new Subject<object>();
    }

    ionViewWillEnter() {
        this.Refresher.next(null);
    }

    GetTypeName(id: number) {
        return UserType[id];
    }
}
