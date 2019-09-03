import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {PipesModule} from '../pipes/pipes-components.module';
import {WareComponentsModule} from '../components/Ware/WareComponents.module';
import {WareComponent} from './ware.component';
import {CalendarWeekModule} from 'angular-calendar';

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        PipesModule,
        RouterModule.forChild([{path: '', component: WareComponent}]),
        WareComponentsModule,
        CalendarWeekModule,
    ],
  declarations: [WareComponent
  ]
})
export class WareModule {}
