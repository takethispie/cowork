import { Component, OnInit } from '@angular/core';
import {TicketState} from '../../../models/TicketState';
import {Field} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {Ticket} from '../../../models/Ticket';
import {DateTime} from 'luxon';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {TicketWareService} from '../../../services/ticket-ware.service';
import {ModalController} from '@ionic/angular';
import {TicketWare} from '../../../models/TicketWare';

@Component({
  selector: 'app-ticket-ware-list',
  templateUrl: './ticket-ware-list.component.html',
  styleUrls: ['./ticket-ware-list.component.scss'],
})
export class TicketWareListComponent implements OnInit {

  data: TicketWare[];
  fields: Field[];

  constructor(public ticketWareService: TicketWareService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: 'ReadonlyText', Name: "Id", Label: "Id", Value: -1 },
      { Type: 'Number', Name: "TicketId", Label: "Id du Ticket", Value: -1},
      { Type: 'Number', Name: "WareId", Label: "Id du matériel", Value: -1}
    ]
  }


  ngOnInit() {
    this.ticketWareService.All().subscribe(res => this.data = res);
  }

  GetTicketState(ind: number) {
    return TicketState[ind];
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new TicketWare();
    model.Id = fieldDic["Id"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    model.WareId = fieldDic["WareId"][0].Value as number;
    return model;
  }

  async UpdateItem(model: TicketWare, fields: Field[]) {
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
      mode === "Update" ? this.ticketWareService.Update(user).subscribe(observer) : this.ticketWareService.Create(user).subscribe(observer);
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.ticketWareService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
