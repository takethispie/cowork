import { Component, Input, OnInit } from '@angular/core';
import {Field, FieldType} from '../Field';
import {DateTime} from "luxon";

@Component({
  selector: 'field-builder',
  template: `
    <div style="margin-bottom: 10px;" [ngSwitch]="GetTypeName(field.Type)">
      <app-select *ngSwitchCase="'Select'" [field]="field" (value)="SetValue($event)"></app-select>
      <ion-item *ngSwitchCase="'Text'">
        <ion-label position="floating">{{ field.Label }}</ion-label>
        <ion-input min="1" [name]="field.Name" [(ngModel)]="field.Value" ngModel></ion-input>
      </ion-item>
      <ion-item *ngSwitchCase="'Number'">
        <ion-label position="floating">{{ field.Label }}</ion-label>
        <ion-input type="number" [name]="field.Name" [(ngModel)]="field.Value" ngModel></ion-input>
      </ion-item>
      <ion-item *ngSwitchCase="'Checkbox'">
        <ion-label>{{field.Label}}</ion-label>
        <ion-checkbox slot="end" [name]="field.Name" [(ngModel)]="field.Value" ngModel></ion-checkbox>
      </ion-item>
      <ion-item *ngSwitchCase="'DatePicker'">
        <ion-label position="floating">{{field.Label}}</ion-label>
        <ion-datetime displayFormat="DD/MM/YYYY" pickerFormat="DD/MM/YYYY" placeholder="Choisir une date" [value]="field.Value" [(ngModel)]="field.Value"
                      max="{{MaxYear()}}" (ionChange)="SetDate($event)" ngModel></ion-datetime>
      </ion-item>
      <ion-item *ngSwitchCase="'DateTimePicker'">
        <ion-label position="floating">{{field.Label}}</ion-label>
        <ion-datetime displayFormat="DD/MM/YYYY HH:mm" pickerFormat="DD/MM/YYYY HH:mm" placeholder="Choisir une date" [value]="field.Value" [(ngModel)]="field.Value"
                      max="{{MaxYear()}}" (ionChange)="SetDate($event)" ngModel></ion-datetime>
      </ion-item>
      <ion-item *ngSwitchCase="'TimePicker'">
        <ion-label position="floating">{{field.Label}}</ion-label>
        <ion-datetime displayFormat="HH:mm" picker-format="HH:mm" placeholder="Choisir une heure" [value]="field.Value" [(ngModel)]="field.Value"
                      (ionChange)="SetDate($event)" ngModel></ion-datetime>
      </ion-item>
      <ion-item *ngSwitchCase="'ReadonlyText'">
        <ion-label position="floating">{{ field.Label }}</ion-label>
        <ion-input [name]="field.Name" readonly [(ngModel)]="field.Value" ngModel></ion-input>
      </ion-item>
      <ion-item *ngSwitchCase="'ReadonlyNumber'">
        <ion-label position="floating">{{ field.Label }}</ion-label>
        <ion-input type="number" [name]="field.Name" readonly [(ngModel)]="field.Value" ngModel></ion-input>
      </ion-item>
    </div>    `
})
export class FieldBuilderComponent implements OnInit {
  @Input() field: Field;

  constructor() { }

  ngOnInit() {
  }

  SetValue(event: string) {
    this.field.Value = event;
  }

  GetTypeName(id: number) {
    return FieldType[id];
  }
  
  MaxYear() {
    return DateTime.local().year + 100 + "";
  }

  SetDate(event: CustomEvent) {
    this.field.Value = event.detail.value as string;
  }
}
