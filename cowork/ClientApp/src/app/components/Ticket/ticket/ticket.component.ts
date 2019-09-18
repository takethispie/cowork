import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {User} from 'src/app/models/User';
import {ToastService} from '../../../services/toast.service';
import {Ticket} from '../../../models/Ticket';
import {DateTime} from 'luxon';
import {TicketState} from '../../../models/TicketState';
import {TicketService} from '../../../services/ticket.service';
import {TicketComment} from '../../../models/TicketComment';
import {LoadingService} from '../../../services/loading.service';
import {TicketCommentService} from '../../../services/ticket-comment.service';
import {AlertController, PopoverController} from '@ionic/angular';
import {UserType} from '../../../models/UserType';
import {TicketAttribution} from '../../../models/TicketAttribution';
import {TicketAttributionService} from '../../../services/ticket-attribution.service';
import {flatMap} from 'rxjs/operators';
import {of} from 'rxjs';
import {ChangeStatusPopoverComponent} from '../change-status-popover/change-status-popover.component';

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


    constructor(private toast: ToastService, private ticketCommentService: TicketCommentService, private loading: LoadingService,
                public alertCtrl: AlertController, public ticketAttributionService: TicketAttributionService,
                public ticketService: TicketService, public popover: PopoverController) {
    }

    ngOnInit() {
        if(this.Ticket.OpenedById === this.authUser.Id) this.UserCanDelete = true;
    }

    public Delete() {
        this.alertCtrl.create({
            header: "Confirmer",
            message: "Supprimer ?",
            buttons: [
                {
                    text: "Annuler",
                    role: "cancel",
                },
                {
                    text: "Oui",
                    handler: () => {
                        this.DeleteTicket.emit(this.Ticket.Id);
                    }
                },
            ],
        }).then(alert => alert.present())
    }

    public CommentSent(commentContent: string) {
        const comment = new TicketComment();
        comment.AuthorId = this.authUser.Id;
        comment.Author = this.authUser;
        comment.Content = commentContent;
        comment.Created = DateTime.local().toISO();
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
        let result = diffs.months.toFixed(0) + " Mois";
        if(diffs.months <= 0) result = diffs.days.toFixed(0) + " jours";
        if(diffs.months <= 0 && diffs.days <= 0) result = diffs.hours.toFixed(0) + " Heures";
        if(diffs.months <= 0 && diffs.days <= 0 && diffs.hours <= 0) result = diffs.minutes.toFixed(0) + " Minutes";
        return result;
    }


    public GetTicketStatus(index: number) {
        switch(index) {
            case 0: return "Nouveau";
            case 1: return "Ouvert";
            case 2: return "En Cours";
            case 3: return "Clos";
            case 4: return "En Retard";
            default: return "Inconnu";

        }
        return TicketState[index];
    }


    public GetUserType(index: number) {
        return UserType[index];
    }


    public GetStatusColor(index: number) {
        switch (index) {
            case 0: return "tertiary";
            case 1: return "dark";
            case 2: return "dark";
            case 3: return "dark";
            case 4: return "danger";
            default: return "dark";
        }
    }


    public AttributeTicketToMe() {
        if(this.Ticket.AttributedTo == null || this.Ticket.AttributedTo.Id === -1) {
            const attr = new TicketAttribution();
            attr.TicketId = this.Ticket.Id;
            attr.StaffId = this.authUser.Id;
            attr.Id = -1;
            this.ticketAttributionService.Create(attr).pipe(
                flatMap(attrResult => {
                    if(attrResult === -1) return of(-1);
                    this.Ticket.AttributedTo = this.authUser;
                    this.Ticket.State = TicketState.Open;
                    return this.ticketService.Update(this.Ticket);
                })
            ).subscribe({
                next: value => {
                    if(value === -1) this.toast.PresentToast("Impossible d'ajouter l'attribution");
                    this.loading.Loading = false;
                },
                error: err => {
                    this.toast.PresentToast("Une erreur est survenue lors de l'attribution du ticket");
                    this.loading.Loading = false;
                },
                complete: () => this.loading.Loading = false
            });
        } else this.toast.PresentToast("Ce ticket est déjà attribué");
    }


    public IsAttributedToMe() {
        if(this.Ticket.AttributedTo == null) return false;
        return this.Ticket.AttributedTo.Id === this.authUser.Id;
    }

    public async OpenChangeStatus() {
        const pop = await this.popover.create({
            component: ChangeStatusPopoverComponent,
        });
        pop.onDidDismiss().then(res => {
            if(res.data == null) return;
            const newTicket = {...this.Ticket};
            newTicket.State = res.data;
            this.loading.Loading = true;
            this.ticketService.Update(newTicket).subscribe({
                next: value => {
                    if(value === -1) this.toast.PresentToast("Impossible de modifier le status");
                    else this.Ticket.State = newTicket.State;
                },
                error: err => {
                    this.toast.PresentToast("Une erreur est survenue lors de la modification du status");
                    this.loading.Loading = false;
                },
                complete: () => this.loading.Loading = false
            })
            console.log(TicketState[res.data]);
        });
        pop.present();
    }
}
