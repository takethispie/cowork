import { Component, OnInit } from '@angular/core';
import {StaffLocationService} from '../../../services/staff-location.service';
import {StaffLocation} from '../../../models/StaffLocation';

@Component({
  selector: 'app-staff-location-list',
  templateUrl: './staff-location-list.component.html',
  styleUrls: ['./staff-location-list.component.scss'],
})
export class StaffLocationListComponent implements OnInit {

  data: StaffLocation[];

  constructor(private staffLocationService: StaffLocationService) { }

  ngOnInit() {
    this.staffLocationService.GetAll().subscribe(res => this.data = res);
  }

}
