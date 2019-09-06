import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { User } from 'src/app/models/User';
import {ToastService} from '../../../services/toast.service';
import {Ticket} from '../../../models/Ticket';
import {DateTime} from 'luxon';
import {TicketState} from '../../../models/TicketState';
import {TicketService} from '../../../services/ticket.service';
import {TicketComment} from '../../../models/TicketComment';
import {LoadingService} from '../../../services/loading.service';
import {TicketCommentService} from "../../../services/ticket-comment.service";

@Component({
    selector: 'ticket-item',
    templateUrl: './ticket.component.html',
    styleUrls: ['./ticket.component.scss'],
})
export class TicketComponent implements OnInit {

    @Input() Ticket: Ticket;
    @Input() authUser: User;
    @Input() ForceEnableDelete: boolean;
    @Output() DeleteTicket: EventEmitter<number> = new EventEmitter();
    public UserCanDelete: boolean = false;


    constructor(private toast: ToastService, private ticketCommentService: TicketCommentService, private loading: LoadingService) {
    }

    ngOnInit() {
        if(this.Ticket.OpenedById === this.authUser.Id) this.UserCanDelete = true;
    }

    Delete() {
        this.DeleteTicket.emit(this.Ticket.Id);
    }

    CommentSent(commentContent: string) {
        const comment = new TicketComment();
        comment.AuthorId = this.authUser.Id;
        comment.Content = commentContent;
        comment.Created = DateTime.local().toHTTP();
        comment.Id = -1;
        comment.TicketId = this.Ticket.Id;
        this.loading.Loading = true;
        this.ticketCommentService.Create(comment).subscribe({
            next: res => {
                console.log(res);
                comment.Id = res;
                this.Ticket.Comments.push(comment);
            },
            error: err => {
                this.loading.Loading = false;
                console.log(err);
            },
            complete: () => this.loading.Loading = false
        })
    }

    public GetCreatedDuration(subDate: DateTime) {
        const now = DateTime.local();
        const diffs = now.diff(subDate.setZone('local'),['months', 'days', 'hours', 'minutes']).toObject();
        let result = diffs.months + " Mois";
        if(diffs.months <= 0) result = diffs.days + " jours";
        if(diffs.months <= 0 && diffs.days <= 0) result = diffs.hours.toFixed(0) + " Heures";
        if(diffs.months <= 0 && diffs.days <= 0 && diffs.hours <= 0) result = diffs.minutes.toFixed(0) + " Minutes";
        return result;
    }

    public GetTicketStatus(index: number) {
        return TicketState[index];
    }

    public GetStatusColor(index: number) {
        switch (index) {
            case 0: return "tertiary";
            case 1: return "medium";
            case 2: return "secondary";
            case 3: return "success";
            case 4: return "danger";
            default: return "dark";
        }
    }
}
