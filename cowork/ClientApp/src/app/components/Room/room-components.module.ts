import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {RoomBookingListComponent} from './room-booking-list/room-booking-list.component';
import {RoomListComponent} from './room-list/room-list.component';
import {RoomCalendarComponent} from './room-calendar/room-calendar.component';
import {CalendarWeekModule} from 'angular-calendar';
import {AddRoomBookingComponent} from './add-room-booking/add-room-booking.component';



@NgModule({
  declarations: [
      RoomBookingListComponent,
      RoomListComponent,
      RoomCalendarComponent,
      AddRoomBookingComponent,
  ],
  exports: [
    RoomBookingListComponent,
    RoomListComponent,
    RoomCalendarComponent,
    AddRoomBookingComponent,
  ],
  entryComponents: [
    RoomBookingListComponent,
    RoomListComponent,
    RoomCalendarComponent,
    AddRoomBookingComponent,
  ],
  imports: [
    CommonModule,
    IonicModule,
    CalendarWeekModule,
  ]
})
export class RoomComponentsModule { }
