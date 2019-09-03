import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {SubscriptionPickerComponent} from './subscription-picker/subscription-picker.component';
import {CommonModule} from '@angular/common';
import {PlaceComponentsModule} from '../Place/place-components.module';
import {SubscriptionCreatorComponent} from './subscription-creator/subscription-creator.component';
import {SubscriptionListComponent} from './subscription-list/subscription-list.component';

@NgModule({
    declarations: [
        SubscriptionPickerComponent,
        SubscriptionCreatorComponent,
        SubscriptionListComponent,
    ],
    entryComponents: [
        SubscriptionPickerComponent,
        SubscriptionCreatorComponent,
        SubscriptionListComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        PlaceComponentsModule,
    ],
    exports: [
        SubscriptionPickerComponent,
        SubscriptionCreatorComponent,
        SubscriptionListComponent,
    ]
})
export class SubscriptionComponentsModule {}
