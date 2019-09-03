import { Component, OnInit } from '@angular/core';
import {Room} from '../../../models/Room';
import {RoomType} from '../../../models/RoomType';

@Component({
  selector: 'app-room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.scss'],
})
export class RoomListComponent implements OnInit {
    data: Room[];

  constructor() { }

  ngOnInit() {}

  GetRoomType(id: number) {
    return RoomType[id];
  }

}
