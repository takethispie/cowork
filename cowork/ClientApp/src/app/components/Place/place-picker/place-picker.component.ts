import { Component, OnInit, Input } from '@angular/core';
import {PlaceService} from '../../../services/place.service';
import {Place} from '../../../models/Place';
import {ModalController} from '@ionic/angular';

@Component({
  selector: 'place-picker',
  templateUrl: './place-picker.component.html',
  styleUrls: ['./place-picker.component.scss'],
})
export class PlacePickerComponent implements OnInit {

  @Input() SelectedId: number;
  public Places: Place[];

  constructor(public place: PlaceService, public modal: ModalController) {
    this.Places = [];
  }

  public LoadData() {
    this.place.All().subscribe(res => this.Places = res);
  }

  ngOnInit() {
    this.LoadData();
  }

  Reload() {
    this.LoadData();
  }

  retour() {
    this.modal.dismiss();
  }

  PlaceChosen(item: Place) {
    this.modal.dismiss(item);
  }
}
