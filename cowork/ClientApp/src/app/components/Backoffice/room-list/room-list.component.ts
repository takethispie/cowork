import {Component, OnInit} from '@angular/core';
import {Room} from '../../../models/Room';
import {RoomType} from '../../../models/RoomType';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {RoomService} from '../../../services/room.service';
import List from 'linqts/dist/src/list';
import {ModalController} from '@ionic/angular';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.scss'],
})
export class RoomListComponent implements OnInit {
  fields: Field[];
  dataHandler: TableDataHandler<Room>;

  constructor(private roomService: RoomService, private modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Text, "Name", "Nom", ""),
      new Field(FieldType.Number, "PlaceId", "Id espace de coworking", -1),
      new Field(FieldType.Select, "Type", "Type de salle", 0, [
        { Label: RoomType[0], Value: 0},
        { Label: RoomType[1], Value: 1}
      ])
    ];
    this.dataHandler = new TableDataHandler<Room>(this.roomService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }


  ngOnInit() {
    this.dataHandler.Refresh();
  }


  GetRoomType(id: number) {
    return RoomType[id];
  }


  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new Room();
    model.Id = fieldDic["Id"][0].Value as number;
    model.Name = fieldDic["Name"][0].Value as string;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    model.Type = fieldDic["Type"][0].Value as number;
    return model;
  }
}
