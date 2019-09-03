import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TicketListComponent} from './ticket-list/ticket-list.component';
import {AddTicketComponent} from './add-ticket/add-ticket.component';
import {FormsModule} from '@angular/forms';
import {CommentComponent} from './comment/comment.component';
import {CommentFormComponent} from './comment-form/comment-form.component';
import {TicketComponent} from './ticket/ticket.component';

@NgModule({
    declarations: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
    ],
    entryComponents: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
    ],
    exports: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
    ]
})
export class TicketComponentsModule {}
