import { Component, OnInit } from '@angular/core';
import {PopoverController} from '@ionic/angular';

@Component({
  selector: 'app-change-status-popover',
  templateUrl: './change-status-popover.component.html',
  styleUrls: ['./change-status-popover.component.scss'],
})
export class ChangeStatusPopoverComponent implements OnInit {

  public Value: number = -1;

  constructor(public popover: PopoverController) { }

  ngOnInit() {}

  public SetValue(val: number) {
    this.Value = val;
    if(this.Value === -1) this.popover.dismiss(null);
    this.popover.dismiss(this.Value);
  }

}
