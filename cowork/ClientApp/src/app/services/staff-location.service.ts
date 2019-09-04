import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {StaffLocation} from '../models/StaffLocation';
import {CONTENTJSON} from "../Utils";

@Injectable({
  providedIn: 'root'
})
export class StaffLocationService {

  constructor(private http: HttpClient) { }

  GetAll() {
    return this.http.get<StaffLocation[]>("api/StaffLocation");
  }
  
  Create(staffLocation: StaffLocation) {
    return this.http.post<number>("api/StaffLocation", staffLocation, CONTENTJSON);
  }
  
  Delete(id: number) {
    return this.http.delete("api/StaffLocation/" + id);
  }
}
