import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {ToFormatedDateTimePipePipe} from './to-formatted-date-time.pipe';
import { ApplyTimeZonePipe } from './apply-time-zone.pipe';
import { ToDateTimePipe } from './to-date-time.pipe';



@NgModule({
    declarations: [
        ToFormatedDateTimePipePipe,
        ApplyTimeZonePipe,
        ToDateTimePipe
    ],
    exports: [
        ToFormatedDateTimePipePipe,
        ApplyTimeZonePipe,
        ToDateTimePipe
    ],
    entryComponents: [
    ],
    imports: [
        CommonModule,
        IonicModule,
    ]
})
export class PipesModule { }
