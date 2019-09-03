import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { MealListComponent } from './meal-list/meal-list.component';
import {MealReservationListComponent} from './meal-reservation-list/meal-reservation-list.component';
import {PipesModule} from '../../pipes/pipes-components.module';



@NgModule({
  declarations: [
    MealListComponent,
    MealReservationListComponent,
  ],
  exports: [
    MealListComponent,
    MealReservationListComponent,
  ],
  entryComponents: [
    MealListComponent,
    MealReservationListComponent,
  ],
  imports: [
    CommonModule,
    IonicModule,
    PipesModule,
  ]
})
export class MealComponentsModule { }
