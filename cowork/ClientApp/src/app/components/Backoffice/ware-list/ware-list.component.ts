import { Component, OnInit } from '@angular/core';
import {Ware} from '../../../models/Ware';
import {WareService} from '../../../services/ware.service';

@Component({
  selector: 'app-ware-list',
  templateUrl: './ware-list.component.html',
  styleUrls: ['./ware-list.component.scss'],
})
export class WareListComponent implements OnInit {

  data: Ware[];

  constructor(private wareService: WareService) { }

  ngOnInit() {
    this.wareService.All().subscribe(res => this.data = res);
  }

}
