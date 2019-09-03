import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PlacePickerComponent} from './place-picker/place-picker.component';
import {IonicModule} from '@ionic/angular';
import {PlaceListComponent} from './place-list/place-list.component';



@NgModule({
    declarations: [
        PlacePickerComponent,
        PlaceListComponent,
    ],
    exports: [
        PlacePickerComponent,
        PlaceListComponent,
    ],
    entryComponents: [
        PlacePickerComponent,
        PlaceListComponent,
    ],
    imports: [
        IonicModule,
        CommonModule
    ]
})
export class PlaceComponentsModule { }
