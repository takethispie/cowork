import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketComponent } from './ticket.component';
import {PipesModule} from '../../pipes/pipes-components.module';
import {TicketComponentsModule} from '../../components/Ticket/TicketComponents.module';
import {WareComponentsModule} from '../../components/Ware/WareComponents.module';

@NgModule({
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        PipesModule,
        RouterModule.forChild([{path: '', component: TicketComponent}]),
        TicketComponentsModule,
        WareComponentsModule
    ],
  declarations: [TicketComponent]
})
export class TicketModule {}
