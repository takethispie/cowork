import {Component, Input, OnInit} from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {UserType} from '../../../models/UserType';

@Component({
  selector: 'comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {

  @Input() TicketComment: TicketComment;

  constructor() { }

  ngOnInit() {}

  IsStaff() {
    return this.TicketComment.Author.Type === UserType.Staff;
  }
}
