import { IonicModule } from '@ionic/angular';
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TicketListComponent} from './ticket-list/ticket-list.component';
import {AddTicketComponent} from './add-ticket/add-ticket.component';
import {FormsModule} from '@angular/forms';
import {CommentComponent} from './comment/comment.component';
import {CommentFormComponent} from './comment-form/comment-form.component';
import {TicketComponent} from './ticket/ticket.component';
import {PipesModule} from '../../pipes/pipes-components.module';
import {UserCommentListComponent} from './user-comment-list/user-comment-list.component';
import {WareComponentsModule} from '../Ware/WareComponents.module';
import {ChangeStatusPopoverComponent} from './change-status-popover/change-status-popover.component';

@NgModule({
    declarations: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
        UserCommentListComponent,
        ChangeStatusPopoverComponent,
    ],
    entryComponents: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
        UserCommentListComponent,
        ChangeStatusPopoverComponent,
    ],
    imports: [
        IonicModule,
        CommonModule,
        FormsModule,
        PipesModule,
        WareComponentsModule,
    ],
    exports: [
        TicketListComponent,
        AddTicketComponent,
        CommentComponent,
        CommentFormComponent,
        TicketComponent,
        UserCommentListComponent,
        ChangeStatusPopoverComponent,
    ]
})
export class TicketComponentsModule {}
