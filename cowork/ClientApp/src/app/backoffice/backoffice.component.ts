import {Component, Input, OnInit} from '@angular/core';
import {DynamicRowDefinition} from '../models/DynamicRowDefinition';
import {UserListComponent} from '../components/Backoffice/user-list/user-list.component';
import {SubscriptionListComponent} from '../components/Backoffice/subscription-list/subscription-list.component';
import {Observable, Subject} from 'rxjs';
import {SubscriptionTypeListComponent} from '../components/Backoffice/subscription-type-list/subscription-type-list.component';
import {PlaceListComponent} from '../components/Backoffice/place-list/place-list.component';
import {MealListComponent} from '../components/Backoffice/meal-list/meal-list.component';
import {MealBookingListComponent} from '../components/Backoffice/meal-booking-list/meal-booking-list.component';
import {RoomBookingListComponent} from '../components/Backoffice/room-booking-list/room-booking-list.component';
import {RoomListComponent} from '../components/Backoffice/room-list/room-list.component';
import {StaffLocationListComponent} from '../components/Backoffice/staff-location-list/staff-location-list.component';
import {TicketAttributionListComponent} from '../components/Backoffice/ticket-attribution-list/ticket-attribution-list.component';
import {TicketCommentListComponent} from '../components/Backoffice/ticket-comment-list/ticket-comment-list.component';
import {TicketListComponent} from '../components/Backoffice/ticket-list/ticket-list.component';
import {TimeSlotListComponent} from '../components/Backoffice/time-slot-list/time-slot-list.component';
import {WareBookingListComponent} from '../components/Backoffice/ware-booking-list/ware-booking-list.component';
import {WareListComponent} from '../components/Backoffice/ware-list/ware-list.component';
import {ModalController} from "@ionic/angular";
import {DynamicFormModalComponent} from "../components/Backoffice/dynamic-form-modal/dynamic-form-modal.component";

@Component({
    selector: 'app-backoffice',
    templateUrl: './backoffice.component.html',
    styleUrls: ['./backoffice.component.scss'],
})
export class BackofficeComponent implements OnInit {

    private definitions: DynamicRowDefinition[] = [];
    private selectedDefinition: string = "User";
    Refresher: Subject<any>;

    constructor(private modalCtrl: ModalController) {
        this.definitions = [
            new DynamicRowDefinition(UserListComponent, {}, "User"),
            new DynamicRowDefinition(SubscriptionListComponent, {}, "Subscription"),
            new DynamicRowDefinition(SubscriptionTypeListComponent, {}, "SubscriptionType"),
            new DynamicRowDefinition(PlaceListComponent, {}, "Place"),
            new DynamicRowDefinition(MealListComponent, {}, "Meal"),
            new DynamicRowDefinition(MealBookingListComponent, {}, "MealBooking"),
            new DynamicRowDefinition(RoomBookingListComponent, {}, "RoomBooking"),
            new DynamicRowDefinition(RoomListComponent, {}, "Room"),
            new DynamicRowDefinition(StaffLocationListComponent, {}, "StaffLocation"),
            new DynamicRowDefinition(TicketAttributionListComponent, {}, "TicketAttribution"),
            new DynamicRowDefinition(TicketCommentListComponent, {}, "TicketComment"),
            new DynamicRowDefinition(TicketListComponent, {}, "Ticket"),
            new DynamicRowDefinition(TimeSlotListComponent, {}, "TimeSlot"),
            new DynamicRowDefinition(WareBookingListComponent, {}, "WareBooking"),
            new DynamicRowDefinition(WareListComponent, {}, "Ware")
        ];
        this.Refresher = new Subject<any>();
    }

    ngOnInit() {}

    Refresh() {

    }

    LoadDataTable(name: string) {
        this.selectedDefinition = name;
        this.Refresher.next(null);
    }

    AddItem(selectedDefinition: string) {
        this.modalCtrl.create({
            component: DynamicFormModalComponent,
        }).then(modal => {
            modal.present();
        })
    }
}
