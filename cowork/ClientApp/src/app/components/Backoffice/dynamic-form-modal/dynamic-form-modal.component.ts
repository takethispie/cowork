import {Component, Input, OnInit} from '@angular/core';
import {NgForm} from "@angular/forms";
import {Field} from "../../dynamic-form-builder/Field";
import {ModalController} from "@ionic/angular";

@Component({
  selector: 'app-dynamic-form-modal',
  templateUrl: './dynamic-form-modal.component.html',
  styleUrls: ['./dynamic-form-modal.component.scss'],
})
export class DynamicFormModalComponent implements OnInit {

  @Input() Fields: Field[] = [];

  constructor(private modal: ModalController) { }

  ngOnInit() {}

  FormSubmitted(event: any) {
    this.modal.dismiss(event);
  }
}
