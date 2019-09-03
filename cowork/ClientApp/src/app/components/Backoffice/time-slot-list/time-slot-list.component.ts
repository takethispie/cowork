import { Component, OnInit } from '@angular/core';
import {TimeSlot} from '../../../models/TimeSlot';
import {TimeSlotService} from '../../../services/time-slot.service';
import {WeekDay} from '@angular/common';

@Component({
  selector: 'app-time-slot-list',
  templateUrl: './time-slot-list.component.html',
  styleUrls: ['./time-slot-list.component.scss'],
})
export class TimeSlotListComponent implements OnInit {

  data: TimeSlot[];

  constructor(private  timeSlotService: TimeSlotService) { }

  ngOnInit() {
    this.timeSlotService.All().subscribe(res => this.data = res);
  }

  GetWeekDay(ind: number) {
     return WeekDay[ind];
  }

}
