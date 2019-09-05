import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {PipesModule} from '../../pipes/pipes-components.module';
import {DynamicDataHostComponent} from './dynamic-data-host/dynamic-data-host.component';
import {AppModule} from '../../app.module';
import {UserListComponent} from './user-list/user-list.component';
import {DataModelHostDirective} from './data-model-host.directive';
import {SubscriptionListComponent} from './subscription-list/subscription-list.component';
import {SubscriptionTypeListComponent} from './subscription-type-list/subscription-type-list.component';
import {PlaceListComponent} from './place-list/place-list.component';
import {MealListComponent} from './meal-list/meal-list.component';
import {MealBookingListComponent} from './meal-booking-list/meal-booking-list.component';
import {RoomBookingListComponent} from './room-booking-list/room-booking-list.component';
import {RoomListComponent} from './room-list/room-list.component';
import {StaffLocationListComponent} from './staff-location-list/staff-location-list.component';
import {TicketCommentListComponent} from './ticket-comment-list/ticket-comment-list.component';
import {TimeSlotListComponent} from './time-slot-list/time-slot-list.component';
import {TicketListComponent} from './ticket-list/ticket-list.component';
import {TicketAttributionListComponent} from './ticket-attribution-list/ticket-attribution-list.component';
import {WareBookingListComponent} from './ware-booking-list/ware-booking-list.component';
import {WareListComponent} from './ware-list/ware-list.component';
import {DynamicFormBuilderModule} from "../dynamic-form-builder/dynamic-form-builder.module";
import {DynamicFormModalComponent} from "./dynamic-form-modal/dynamic-form-modal.component";
import {TicketWareListComponent} from "./ticket-ware-list/ticket-ware-list.component";

@NgModule({
  declarations: [
      DynamicDataHostComponent,
      UserListComponent,
      DataModelHostDirective,
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
      TicketWareListComponent,
  ],
  exports: [
      DynamicDataHostComponent,
      UserListComponent,
      DataModelHostDirective,
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
      TicketWareListComponent,
  ],
  entryComponents: [
      DynamicDataHostComponent,
      UserListComponent,
      DynamicFormModalComponent,
  ],
    imports: [
        CommonModule,
        IonicModule,
        PipesModule,
        DynamicFormBuilderModule,
    ]
})
export class BackofficeComponentsModule { }
