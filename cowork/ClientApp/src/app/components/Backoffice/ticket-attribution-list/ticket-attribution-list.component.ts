import {Component, OnInit} from '@angular/core';
import {TicketAttribution} from '../../../models/TicketAttribution';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import List from 'linqts/dist/src/list';
import {DynamicFormModalComponent} from '../dynamic-form-modal/dynamic-form-modal.component';
import {ModalController} from '@ionic/angular';
import {TicketAttributionService} from '../../../services/ticket-attribution.service';

@Component({
  selector: 'app-ticket-attribution-list',
  templateUrl: './ticket-attribution-list.component.html',
  styleUrls: ['./ticket-attribution-list.component.scss'],
})
export class TicketAttributionListComponent implements OnInit {

  data: TicketAttribution[];
  fields: Field[];
  
  constructor(public ApiService: TicketAttributionService, public modalCtrl: ModalController) { 
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "StaffId", "Id du personnel", -1),
      new Field(FieldType.Number, "TicketId", "Id du ticket", -1)
    ];
  }

  ngOnInit() {
    this.ApiService.All().subscribe(res => this.data = res);
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new TicketAttribution();
    model.Id = fieldDic["Id"][0].Value as number;
    model.StaffId = fieldDic["StaffId"][0].Value as number;
    model.TicketId = fieldDic["TicketId"][0].Value as number;
    return model;
  }

  async UpdateItem(model: TicketAttribution, fields: Field[]) {
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
