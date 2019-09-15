import {Component, Input, OnInit} from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {ToastService} from '../../../services/toast.service';
import {LoadingService} from '../../../services/loading.service';
import {AuthService} from '../../../services/auth.service';
import {TicketCommentService} from '../../../services/ticket-comment.service';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';

@Component({
  selector: 'user-comment-list',
  templateUrl: './user-comment-list.component.html',
  styleUrls: ['./user-comment-list.component.scss'],
})
export class UserCommentListComponent implements OnInit {

  public Comments: TicketComment[];
  @Input() Refresher: Observable<object>;

  constructor(public ticketCommentService: TicketCommentService, public toast: ToastService, public loader: LoadingService, public auth: AuthService) {

  }

  ngOnInit() {
    this.Refresher.subscribe({
      next: () => this.LoadData()
    });
  }


  public LoadData() {
    this.loader.Loading = true;
    this.ticketCommentService.LastCommentsFromUser(this.auth.UserId, 10).pipe(
        map(comments => comments.filter(comment => comment.AuthorId === this.auth.UserId))
    ).subscribe({
      next: value => {
        this.Comments = value;
      }
    });
  }

}
