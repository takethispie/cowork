import { Component, OnInit } from '@angular/core';
import {TimeSlot} from '../../../models/TimeSlot';
import {TimeSlotService} from '../../../services/time-slot.service';
import {WeekDay} from '@angular/common';
import {Field} from "../../dynamic-form-builder/Field";
import {ModalController} from "@ionic/angular";
import List from "linqts/dist/src/list";
import {Ticket} from "../../../models/Ticket";
import {DateTime} from "luxon";
import {DynamicFormModalComponent} from "../dynamic-form-modal/dynamic-form-modal.component";

@Component({
  selector: 'app-time-slot-list',
  templateUrl: './time-slot-list.component.html',
  styleUrls: ['./time-slot-list.component.scss'],
})
export class TimeSlotListComponent implements OnInit {

  data: TimeSlot[];
  fields: Field[];

  constructor(private  timeSlotService: TimeSlotService, public modalCtrl: ModalController) {
    this.fields = [
      { Type: "Text", Name: "PlaceId", Label: "Id de l'espace de corworking", Value: null},
      { Type: "TimePicker", Name: "Start", Label: "Heure d'ouverture", Value: "08:00"},
      { Type: "TimePicker", Name: "End", Label: "Heure de fermeture", Value: "20:00"},
      { Type: "Select", Name: "Day", Label: "Jour", Value: 0, Options: [
          { Label: WeekDay[1], Value: WeekDay.Monday},
          { Label: WeekDay[2], Value: WeekDay.Tuesday},
          { Label: WeekDay[3], Value: WeekDay.Wednesday},
          { Label: WeekDay[4], Value: WeekDay.Thursday},
          { Label: WeekDay[5], Value: WeekDay.Friday},
          { Label: WeekDay[6], Value: WeekDay.Saturday},
          { Label: WeekDay[0], Value: WeekDay.Sunday},
        ]
      }
    ]
  }

  ngOnInit() {
    this.timeSlotService.All().subscribe(res => this.data = res);
  }

  GetWeekDay(ind: number) {
     return WeekDay[ind];
  }
  
  PadTime(hour: number) {
    return (hour + "").padStart(2, '0');
  }

  CreateModelFromFields(fields: Field[]) {
    const fieldDic = new List(fields).GroupBy(f => f.Name);
    let model = new TimeSlot();
    model.Id = -1;
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

  async AddItem() {
    const modal = await this.modalCtrl.create({
      component: DynamicFormModalComponent,
      componentProps: { Fields: this.fields }
    });
    modal.onDidDismiss().then(res => {
      if(res.data == null) return;
      const model = this.CreateModelFromFields(this.fields);
      this.timeSlotService.Create(model).subscribe({
        next: value => this.ngOnInit(),
        error: err => console.log(err)
      });
    });
    await modal.present();
  }

  async Delete(id: number) {
    this.timeSlotService.Delete(id).subscribe({
      next: value => this.ngOnInit(),
      error: err => {}
    });
  }

}
