import { Component, OnInit } from '@angular/core';
import {Ticket} from '../../../models/Ticket';
import {TicketService} from '../../../services/ticket.service';
import {TicketState} from '../../../models/TicketState';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.scss'],
})
export class TicketListComponent implements OnInit {

  data: Ticket[];
  fields: Field[];

  constructor(private ticketService: TicketService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "Title", Label: "Titre", Value: null},
      { Type: "Text", Name: "Description", Label: "Description", Value: ""},
      { Type: "Text", Name: "UserId", Label: "Auteur du ticket", Value: null},
      { Type: "Select", Name: "State", Label: "Status du ticket", Value: 0, Options: [
          { Label: TicketState[0], Value: 0},
          { Label: TicketState[1], Value: 1},
          { Label: TicketState[2], Value: 2},
          { Label: TicketState[3], Value: 3},
          { Label: TicketState[4], Value: 4}
        ]
      }
    ];
  }

  ngOnInit() {
    this.ticketService.All().subscribe(res => this.data = res);
  }

  GetTicketState(ind: number) {
    return TicketState[ind];
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Ticket();
    model.Id = -1;
    model.Title = fieldDic["Title"][0].Value as string;
    model.Description = fieldDic["Description"][0].Value as string;
    model.Created = DateTime.local();
    model.OpenedById = fieldDic["UserId"][0].Value as number;
    model.State = fieldDic["State"][0].Value as number;
    return model;
  }

  async UpdateItem(model: Ticket, fields: Field[]) {
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
      mode === "Update" ? this.ticketService.Update(user).subscribe(observer) : this.ticketService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.ticketService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
