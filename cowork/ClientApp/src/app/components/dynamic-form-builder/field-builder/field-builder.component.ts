import { Component, Input, OnInit } from '@angular/core';
import {Field} from "../Field";

@Component({
  selector: 'field-builder',
  template: `
    <div style="margin-bottom: 10px;" [ngSwitch]="field.Type">
      <app-select *ngSwitchCase="'Radio'" [field]="field" (value)="SetValue($event)"></app-select>
      <ion-item *ngSwitchCase="'Text'">
        <ion-label>{{ field.Label }}</ion-label>
        <ion-input [name]="field.Name"  [(ngModel)]="field.Value" ngModel></ion-input>
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
}
