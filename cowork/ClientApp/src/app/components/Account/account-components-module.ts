import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {PipesModule} from '../../pipes/pipes-components.module';
import {AccountAdminComponent} from './account-admin/account-admin.component';
import {AccountUserComponent} from './account-user/account-user.component';
import {AccountStaffComponent} from './account-staff/account-staff.component';
import {FormsModule} from '@angular/forms';
import {MealComponentsModule} from '../Meal/meal-components.module';
import {RoomComponentsModule} from '../Room/room-components.module';
import {TicketComponentsModule} from '../Ticket/TicketComponents.module';
import {BackofficeComponentsModule} from '../Backoffice/backoffice-components.module';



@NgModule({
    declarations: [
        AccountAdminComponent,
        AccountUserComponent,
        AccountStaffComponent,
    ],
    exports: [
        AccountAdminComponent,
        AccountUserComponent,
        AccountStaffComponent,
    ],
    entryComponents: [
        AccountAdminComponent,
        AccountUserComponent,
        AccountStaffComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        IonicModule,
        PipesModule,
        MealComponentsModule,
        RoomComponentsModule,
        TicketComponentsModule,
        BackofficeComponentsModule,
    ]
})
export class AccountComponentsModule { }
