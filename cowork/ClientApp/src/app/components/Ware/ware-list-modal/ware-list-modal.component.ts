import {Component, Input, OnInit} from '@angular/core';
import {ModalController} from '@ionic/angular';
import {Ware} from '../../../models/Ware';
import {Subject} from 'rxjs';

@Component({
  selector: 'app-ware-list-modal',
  templateUrl: './ware-list-modal.component.html',
  styleUrls: ['./ware-list-modal.component.scss'],
})
export class WareListModalComponent implements OnInit {

  @Input() PlaceId: number;
  @Input() UserId: number;
  public Refresher: Subject<void>;

  constructor(public modalCtrl: ModalController) {
    this.Refresher = new Subject<void>();
  }

  ngOnInit() {}


  retour() {
    this.modalCtrl.dismiss(null);
  }


  Reload() {
    this.Refresher.next();
  }


  Selected(ware: Ware) {
    this.modalCtrl.dismiss(ware);
  }



}
