import {Component, OnInit} from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {DateTime} from 'luxon';
import {TicketCommentService} from '../../../services/ticket-comment.service';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-ticket-comment-list',
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.scss'],
})
export class TicketCommentListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<TicketComment>;

  constructor(private ApiService: TicketCommentService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "UserId", "Id utilisateur", -1),
      new Field(FieldType.Number, "TicketId", "Id du ticket", -1),
      new Field(FieldType.Text, "Content", "Texte du commentaire", "")
    ];
    this.dataHandler = new TableDataHandler<TicketComment>(this.ApiService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new TicketComment();
    model.Id = fieldDic["Id"][0].Value as number;
    model.AuthorId = fieldDic["UserId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    model.Content = fieldDic["Content"][0].Value as string;
    model.Created = DateTime.local().toFormat("dd/MM/yyyy HH:mm");
    return model;
  }
}
