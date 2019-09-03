import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.scss'],
})
export class CommentFormComponent implements OnInit {

  @Output() CommentSent: EventEmitter<string> = new EventEmitter<string>();

  public message: string;

  constructor() {}

  ngOnInit() {}

  public sendComment() {
    if(this.message == null) return;
    this.CommentSent.next(this.message);
    this.message = "";
  }
}
