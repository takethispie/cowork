import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReservationTabPage } from './reservation.component';
import {RoomComponentsModule} from '../../components/Room/room-components.module';

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        RouterModule.forChild([{path: '', component: ReservationTabPage}]),
        RoomComponentsModule
    ],
  declarations: [ReservationTabPage]
})
export class ReservationModule {}
