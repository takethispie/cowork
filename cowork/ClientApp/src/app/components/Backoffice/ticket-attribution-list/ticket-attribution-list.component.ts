import { Component, OnInit } from '@angular/core';
import {TicketAttribution} from '../../../models/TicketAttribution';
import {TicketService} from '../../../services/ticket.service';
import {Field} from "../../dynamic-form-builder/Field";
import List from "linqts/dist/src/list";
import {Subscription} from "../../../models/Subscription";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";
import {Ticket} from "../../../models/Ticket";

@Component({
  selector: 'app-ticket-attribution-list',
  templateUrl: './ticket-attribution-list.component.html',
  styleUrls: ['./ticket-attribution-list.component.scss'],
})
export class TicketAttributionListComponent implements OnInit {

  data: TicketAttribution[];
  fields: Field[];
  
  constructor(public ticketService: TicketService, public modalCtrl: ModalController) { 
    this.fields = [
      { Type: "Text", Name: "StaffId", Label: "Id du personnel", Value: null},
      { Type: "Text", Name: "TicketId", Label: "Id du ticket", Value: null}  
    ];
  }

  ngOnInit() {
    this.ticketService.AllAttribution().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new TicketAttribution();
    model.Id = -1;
    model.StaffId = fieldDic["StaffId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
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
      this.ticketService.CreateAttribution(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.ticketService.DeleteAttribution(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
