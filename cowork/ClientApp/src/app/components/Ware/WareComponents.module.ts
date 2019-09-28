import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {WareListComponent} from './ware-list/ware-list.component';
import {WareListModalComponent} from './ware-list-modal/ware-list-modal.component';

@NgModule({
    declarations: [
        WareListComponent,
        WareListModalComponent,
    ],
    entryComponents: [
        WareListComponent,
        WareListModalComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
    ],
    exports: [
        WareListComponent,
        WareListModalComponent,
    ]
})
export class WareComponentsModule {}
