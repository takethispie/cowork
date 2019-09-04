import { Component, Input, OnInit } from '@angular/core';
import {Field} from "../Field";
import {DateTime} from "luxon";

@Component({
  selector: 'field-builder',
  template: `
    <div style="margin-bottom: 10px;" [ngSwitch]="field.Type">
      <app-select *ngSwitchCase="'Select'" [field]="field" (value)="SetValue($event)"></app-select>
      <ion-item *ngSwitchCase="'Text'">
        <ion-label>{{ field.Label }}</ion-label>
        <ion-input [name]="field.Name"  [(ngModel)]="field.Value" ngModel></ion-input>
      </ion-item>
      <ion-item *ngSwitchCase="'Checkbox'">
        <ion-label>{{field.Label}}</ion-label>
        <ion-checkbox slot="end" [name]="field.Name" [(ngModel)]="field.Value" ngModel></ion-checkbox>
      </ion-item>
      <ion-item *ngSwitchCase="'DatePicker'">
        <ion-label>{{field.Label}}</ion-label>
        <ion-datetime displayFormat="DD/MM/YYYY" pickerFormat="DD/MM/YYYY HH:mm" placeholder="Choisir une date" [value]="field.Value" max="{{MaxYear()}}"></ion-datetime>
      </ion-item>
      <ion-item *ngSwitchCase="'DateTimePicker'">
        <ion-label>{{field.Label}}</ion-label>
        <ion-datetime displayFormat="DD/MM/YYYY HH:mm" pickerFormat="DD/MM/YYYY HH:mm" placeholder="Choisir une date" [value]="field.Value" max="{{MaxYear()}}"></ion-datetime>
      </ion-item>
    </div>
  `
})
export class FieldBuilderComponent implements OnInit {
  @Input() field: Field;

  constructor() { }

  ngOnInit() {
  }

  SetValue(event: string) {
    this.field.Value = event;
  }
  
  MaxYear() {
    return DateTime.local().year + 100 + "";
  }
}
