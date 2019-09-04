import { Component, OnInit } from '@angular/core';
import {Room} from '../../../models/Room';
import {RoomType} from '../../../models/RoomType';
import {Field} from "../../dynamic-form-builder/Field";
import {RoomService} from "../../../services/room.service";
import List from "linqts/dist/src/list";
import {RoomBooking} from "../../../models/RoomBooking";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.scss'],
})
export class RoomListComponent implements OnInit {
    data: Room[];
    fields: Field[];

  constructor(private roomService: RoomService, private modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "Name", Label: "Nom", Value: null},
      { Type: "Text", Name: "PlaceId", Label: "Id de l'espace de coworking", Value: null},
      { Type: "Select", Name: "Type", Label: "Type de salle", Value: "Call", Options: [
          { Label: RoomType[0], Value: 0},
          { Label: RoomType[1], Value: 1}
        ]
      }
    ]
  }

  ngOnInit() {
    this.roomService.All().subscribe({
      next: value => this.data = value
    });
  }

  GetRoomType(id: number) {
    return RoomType[id];
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new Room();
    model.Id = -1;
    model.Name = fieldDic["Name"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.Type = fieldDic["Type"][0].Value as number;
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
      this.roomService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.roomService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
