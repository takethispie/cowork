import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CONTENTJSON} from "../Utils";
import {Place} from "../models/Place";

@Injectable({
  providedIn: 'root'
})
export class PlaceService {

  constructor(public http: HttpClient) { }

  public All() {
    return this.http.get<Place[]>("api/Place");
  }


  public Create(place: Place) {
    return this.http.post<number>("api/Place", place, CONTENTJSON);
  }


  public Update(place: Place) {
    return this.http.put<number>("api/Place", place, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Place/ById/" + id);
  }


  public DeleteByName(name: string) {
    return this.http.delete("api/Place/ByName/" + name);
  }


  public ById(id: number) {
    return this.http.get<Place>("api/Place/ById/" + id);
  }


  public ByName(name: string) {
    return this.http.get<Place>("api/Place/ByName/" + name);
  }


  public AllWithPaging(page: number, amount: number) {
      return this.http.get<Place[]>("api/Place/WithPaging/" + page + "/" + amount);
  }
}
