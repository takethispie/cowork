import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {PipesModule} from '../pipes/pipes-components.module';
import {BackofficeComponent} from './backoffice.component';
import {BackofficeComponentsModule} from '../components/Backoffice/backoffice-components.module';
import {UserListComponent} from '../components/Backoffice/user-list/user-list.component';
import {SubscriptionListComponent} from '../components/Backoffice/subscription-list/subscription-list.component';
import {SubscriptionTypeListComponent} from '../components/Backoffice/subscription-type-list/subscription-type-list.component';
import {PlaceListComponent} from '../components/Backoffice/place-list/place-list.component';
import {MealListComponent} from '../components/Backoffice/meal-list/meal-list.component';
import {MealBookingListComponent} from '../components/Backoffice/meal-booking-list/meal-booking-list.component';
import {RoomBookingListComponent} from '../components/Backoffice/room-booking-list/room-booking-list.component';
import {RoomListComponent} from '../components/Backoffice/room-list/room-list.component';
import {StaffLocationListComponent} from '../components/Backoffice/staff-location-list/staff-location-list.component';
import {TicketCommentListComponent} from '../components/Backoffice/ticket-comment-list/ticket-comment-list.component';
import {TimeSlotListComponent} from '../components/Backoffice/time-slot-list/time-slot-list.component';
import {TicketListComponent} from '../components/Backoffice/ticket-list/ticket-list.component';
import {TicketAttributionListComponent} from '../components/Backoffice/ticket-attribution-list/ticket-attribution-list.component';
import {WareBookingListComponent} from '../components/Backoffice/ware-booking-list/ware-booking-list.component';
import {WareListComponent} from '../components/Backoffice/ware-list/ware-list.component';
import {DynamicFormBuilderModule} from "../components/dynamic-form-builder/dynamic-form-builder.module";
import {DynamicFormModalComponent} from "../components/Backoffice/dynamic-form-modal/dynamic-form-modal.component";

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        PipesModule,
        RouterModule.forChild([{path: '', component: BackofficeComponent}]),
        BackofficeComponentsModule,
        DynamicFormBuilderModule,
    ],
    entryComponents: [
        UserListComponent,
        SubscriptionListComponent,
        SubscriptionTypeListComponent,
        PlaceListComponent,
        MealListComponent,
        MealBookingListComponent,
        RoomBookingListComponent,
        RoomListComponent,
        StaffLocationListComponent,
        TicketCommentListComponent,
        TimeSlotListComponent,
        TicketListComponent,
        TicketAttributionListComponent,
        WareBookingListComponent,
        WareListComponent,
        DynamicFormModalComponent,
    ],
    declarations: [BackofficeComponent]
})
export class BackofficeModule {}
