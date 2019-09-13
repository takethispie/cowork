import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {WareListComponent} from './ware-list/ware-list.component';
import {WareBookingCalendarComponent} from './ware-booking-calendar/ware-booking-calendar.component';
import {WareListModalComponent} from './ware-list-modal/ware-list-modal.component';

@NgModule({
    declarations: [
        WareListComponent,
        WareBookingCalendarComponent,
        WareListModalComponent,
    ],
    entryComponents: [
        WareListComponent,
        WareBookingCalendarComponent,
        WareListModalComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
    ],
    exports: [
        WareListComponent,
        WareBookingCalendarComponent,
        WareListModalComponent,
    ]
})
export class WareComponentsModule {}
