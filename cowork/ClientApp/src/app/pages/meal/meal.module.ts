import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MealComponent } from './meal.component';
import {MealComponentsModule} from '../../components/Meal/meal-components.module';
import {PipesModule} from '../../pipes/pipes-components.module';

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        PipesModule,
        RouterModule.forChild([{path: '', component: MealComponent}]),
        MealComponentsModule
    ],
  declarations: [MealComponent]
})
export class MealModule {}
