import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Field} from "../../Field";

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss'],
})
export class SelectComponent implements OnInit {
  @Input() field: Field;
  @Output() value: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {}

  ValueChanged(event: CustomEvent) {
    console.log(event);
    this.value.emit(event.detail.value);
  }
}
