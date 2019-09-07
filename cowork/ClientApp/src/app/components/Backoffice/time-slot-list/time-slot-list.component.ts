import {Component, OnInit} from '@angular/core';
import {TimeSlot} from '../../../models/TimeSlot';
import {TimeSlotService} from '../../../services/time-slot.service';
import {WeekDay} from '@angular/common';
import {Field, FieldType} from '../../dynamic-form-builder/Field';
import {ModalController} from '@ionic/angular';
import List from 'linqts/dist/src/list';
import {DateTime} from 'luxon';
import {TableDataHandler} from '../TableDataHandler';

@Component({
  selector: 'app-time-slot-list',
  templateUrl: './time-slot-list.component.html',
  styleUrls: ['./time-slot-list.component.scss'],
})
export class TimeSlotListComponent implements OnInit {

  fields: Field[];
  dataHandler: TableDataHandler<TimeSlot>;

  constructor(private  timeSlotService: TimeSlotService, public modalCtrl: ModalController) {
    this.fields = [
      new Field(FieldType.ReadonlyNumber, "Id", "Id", -1),
      new Field(FieldType.Number, "PlaceId", "Id espace de coworking", -1),
      new Field(FieldType.TimePicker,"Start", "Heure d'ouverture", "08:00"),
      new Field(FieldType.TimePicker, "End", "Heure de fermeture", "20:00"),
      new Field(FieldType.Select, "Day", "Jour", 0, [
          { Label: WeekDay[1], Value: WeekDay.Monday},
          { Label: WeekDay[2], Value: WeekDay.Tuesday},
          { Label: WeekDay[3], Value: WeekDay.Wednesday},
          { Label: WeekDay[4], Value: WeekDay.Thursday},
          { Label: WeekDay[5], Value: WeekDay.Friday},
          { Label: WeekDay[6], Value: WeekDay.Saturday},
          { Label: WeekDay[0], Value: WeekDay.Sunday},
        ]
      )
    ];
    this.dataHandler = new TableDataHandler<TimeSlot>(this.timeSlotService, this.modalCtrl, this.fields, this.CreateModelFromFields);
  }

  ngOnInit() {
    this.dataHandler.Refresh();
  }

  GetWeekDay(ind: number) {
     return WeekDay[ind];
  }
  
  PadTime(hour: number) {
    return (hour + "").padStart(2, '0');
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    const model = new TimeSlot();
    model.Id = fieldDic["Id"][0].Value as number;
    model.PlaceId = fieldDic["PlaceId"][0].Value as number;
    const start = DateTime.fromFormat(fieldDic["Start"][0].Value as string, "HH:mm");
    const end = DateTime.fromFormat(fieldDic["End"][0].Value as string, "HH:mm");
    model.StartHour = start.hour;
    model.StartMinutes = start.minute;
    model.EndHour = end.hour;
    model.EndMinutes = end.minute;
    model.Day = fieldDic["Day"][0].Value as number;
    return model;
  }
}
