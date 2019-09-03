import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StaffLocation} from '../models/StaffLocation';

@Injectable({
  providedIn: 'root'
})
export class StaffLocationService {

  constructor(private http: HttpClient) { }

  GetAll() {
    return this.http.get<StaffLocation[]>("api/StaffLocation");
  }
}
