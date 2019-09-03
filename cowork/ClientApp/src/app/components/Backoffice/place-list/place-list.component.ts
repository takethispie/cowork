import { Component, OnInit } from '@angular/core';
import {Place} from '../../../models/Place';
import {PlaceService} from '../../../services/place.service';

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.scss'],
})
export class PlaceListComponent implements OnInit {
  data: Place[];

  constructor(private placeService: PlaceService) { }

  ngOnInit() {
    this.placeService.All().subscribe(res => this.data = res);
  }

}
