import { Component, OnInit, Input } from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';

@Component({
  selector: 'comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {

  @Input() TicketComment: TicketComment;

  constructor() { }

  ngOnInit() {}
}
