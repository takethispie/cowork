import { Component, OnInit } from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {TicketService} from '../../../services/ticket.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {TicketAttribution} from "../../../models/TicketAttribution";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";

@Component({
  selector: 'app-ticket-comment-list',
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.scss'],
})
export class TicketCommentListComponent implements OnInit {

  data: TicketComment[];
  fields: Field[];

  constructor(private ticketService: TicketService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: 'UserId', Label: "Id de l'utilisateur", Value: null},
      { Type: "Text", Name: "TicketId", Label: "Id du ticket", Value: null},
      { Type: "Text", Name: "Content", Label: "Texte du commentaire", Value: null},
    ];
  }

  ngOnInit() {
    this.ticketService.AllComments().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new TicketComment();
    model.Id = -1;
    model.AuthorId = fieldDic["UserId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    model.Content = fieldDic["Content"][0].Value as string;
    model.Created = DateTime.local().toFormat("dd/MM/yyyy HH:mm");
    return model;
  }

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const model = this.CreateModelFromFields(this.fields);
      this.ticketService.AddComment(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.ticketService.DeleteComment(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
