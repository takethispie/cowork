import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import {ToFormatedDateTimePipePipe} from './to-date-time.pipe';



@NgModule({
    declarations: [
        ToFormatedDateTimePipePipe
    ],
    exports: [
        ToFormatedDateTimePipePipe
    ],
    entryComponents: [
    ],
    imports: [
        CommonModule,
        IonicModule,
    ]
})
export class PipesModule { }
