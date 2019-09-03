import {Component, Input, OnInit} from '@angular/core';
import {DynamicRowDefinition} from '../models/DynamicRowDefinition';
import {UserListComponent} from '../components/Backoffice/user-list/user-list.component';
import {SubscriptionListComponent} from '../components/Backoffice/subscription-list/subscription-list.component';
import {Observable, of, Subject} from 'rxjs';
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
import {Field} from "../components/dynamic-form-builder/Field";
import {User} from "../models/User";
import {UserService} from "../services/user.service";
import List from "linqts/dist/src/list";

@Component({
    selector: 'app-backoffice',
    templateUrl: './backoffice.component.html',
    styleUrls: ['./backoffice.component.scss'],
})
export class BackofficeComponent implements OnInit {

    private definitions: DynamicRowDefinition[] = [];
    private selectedDefinition: string = "User";
    private Refresher: Subject<any>;

    public AddLambda: <T>(model: T) => Observable<number>;
    
    constructor(private modalCtrl: ModalController, private userService: UserService) {
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

    async AddItem(selectedDefinition: string) {
        let fields: Field[];
        let BuilderLambda: (fields: Field[]) => any;
        switch (selectedDefinition) {
            case "User":
                this.AddLambda = <User>(model) => {
                    return this.userService.Create(model)
                };
                BuilderLambda = (fields: Field[]) => {
                    const fieldDic = new List(fields).GroupBy(f => f.Name);
                    let user = new User();
                    user.LastName = fieldDic["LastName"][0].Value as string;
                    user.FirstName = fieldDic["FirstName"][0].Value as string;
                    user.Id = -1;
                    user.IsAStudent = fieldDic["IsAStudent"][0].Value as boolean;
                    user.Email = fieldDic["Email"][0].Value as string;
                    return user;
                };
                fields = [
                    { Type: "Text", Name: "FirstName", Value: "", Label: "Prénom" },
                    { Type: "Text", Name: "LastName", Value: "", Label: "Nom"},
                    { Type: "Text", Name: "Email", Value: "", Label: "Email"},
                    { Type: "Select", Name: "Type", Value: "User", Label: "Type d'utilisateur", Options: [
                            { Label: "User", Value: 0 },
                            { Label: "Staff", Value: 1 },
                            { Label: "Admin", Value: 2 }
                        ]
                    },
                    { Type: "Checkbox", Name: "IsAStudent", Value: false, Label: "Est Étudiant"}
                ];
                break;
        }
        const modal = await this.modalCtrl.create({
            component: DynamicFormModalComponent,
            componentProps: { Fields: fields }
        });
        modal.onDidDismiss().then(res => {
            console.log(res.data);
            const user = BuilderLambda(res.data);
            this.AddLambda(user).subscribe({
                next: value => console.log(value),
                error: err => console.log(err)
            });
        });
        modal.present();

    }
}
