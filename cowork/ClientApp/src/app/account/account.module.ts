import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountComponent } from './account.component';
import {SubscriptionComponentsModule} from '../components/Subscription/SubscriptionComponents.module';
import {PlaceComponentsModule} from '../components/Place/place-components.module';
import {MealComponentsModule} from '../components/Meal/meal-components.module';
import {RoomComponentsModule} from '../components/Room/room-components.module';
import {PipesModule} from '../pipes/pipes-components.module';
import {WareComponentsModule} from '../components/Ware/WareComponents.module';

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        SubscriptionComponentsModule,
        PlaceComponentsModule,
        MealComponentsModule,
        RoomComponentsModule,
        PipesModule,
        RouterModule.forChild([{path: '', component: AccountComponent}]),
        WareComponentsModule
    ],
    declarations: [AccountComponent]
})
export class AccountModule {}
