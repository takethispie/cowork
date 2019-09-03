import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {Field} from "./Field";

@Component({
  selector: 'dynamic-form-builder',
  template:`      
    <form #form="ngForm" (ngSubmit)="onSubmit.emit(fields)">
      <field-builder *ngFor="let field of fields" [field]="field"></field-builder>
      <div padding>
        <ion-button size="large" type="submit" [disabled]="form.invalid" expand="block">Ajouter</ion-button>
      </div>
    </form>
  `,
})
export class DynamicFormBuilderComponent implements OnInit {
  @Output() onSubmit = new EventEmitter();
  @Input() fields: Field[] = [];
  
  constructor() { }

  ngOnInit() {
    
  }
}
