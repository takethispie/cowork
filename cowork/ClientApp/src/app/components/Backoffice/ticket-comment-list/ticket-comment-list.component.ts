import { Component, OnInit } from '@angular/core';
import {TicketComment} from '../../../models/TicketComment';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {DateTime} from "luxon";
import {TicketCommentService} from "../../../services/ticket-comment.service";

@Component({
  selector: 'app-ticket-comment-list',
  templateUrl: './ticket-comment-list.component.html',
  styleUrls: ['./ticket-comment-list.component.scss'],
})
export class TicketCommentListComponent implements OnInit {

  data: TicketComment[];
  fields: Field[];

  constructor(private ApiService: TicketCommentService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "ReadonlyText", Name: "Id", Label: "Id", Value: "-1"},
      { Type: "Text", Name: 'UserId', Label: "Id de l'utilisateur", Value: null},
      { Type: "Text", Name: "TicketId", Label: "Id du ticket", Value: null},
      { Type: "Text", Name: "Content", Label: "Texte du commentaire", Value: null},
    ];
  }

  ngOnInit() {
    this.ApiService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new TicketComment();
    model.Id = fieldDic["Id"][0].Value as number;
    model.AuthorId = fieldDic["UserId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    model.Content = fieldDic["Content"][0].Value as string;
    model.Created = DateTime.local().toFormat("dd/MM/yyyy HH:mm");
    return model;
  }

  async UpdateItem(model: TicketComment, fields: Field[]) {
    const fieldList = new List(fields).Select(field => {
      field.Value = model[field.Name];
      return field;
    });
    await this.OpenModal("Update", { Fields: fieldList.ToArray()});
  }

  async AddItem() {
    await this.OpenModal("Create", { Fields: this.fields });
  }

  async OpenModal(mode: "Update" | "Create" ,componentProps: any) {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const user = this.CreateModelFromFields(this.fields);
      const observer = {
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      };
      mode === "Update" ? this.ApiService.Update(user).subscribe(observer) : this.ApiService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.ApiService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
