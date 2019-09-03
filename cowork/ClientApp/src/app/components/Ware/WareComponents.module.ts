import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {WareListComponent} from './ware-list/ware-list.component';
import {WareBookingCalendarComponent} from './ware-booking-calendar/ware-booking-calendar.component';

@NgModule({
    declarations: [
        WareListComponent,
        WareBookingCalendarComponent,
    ],
    entryComponents: [
        WareListComponent,
        WareBookingCalendarComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
    ],
    exports: [
        WareListComponent,
        WareBookingCalendarComponent,
    ]
})
export class WareComponentsModule {}
