import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Place} from "../models/Place";
import {CONTENTJSON} from "../Utils";
import {Room} from "../models/Room";

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(public http: HttpClient) { }

  public All() {
    return this.http.get<Room[]>("api/Room");
  }


  public AllWithPaging(page: number, amount: number) {
    return this.http.get<Room[]>("api/Rool/WithPaging/" + page + "/" + amount);
  }


  public Create(room: Room) {
    return this.http.post<number>("api/Room", room, CONTENTJSON);
  }


  public Update(room: Room) {
    return this.http.put<number>("api/Room", room, CONTENTJSON);
  }


  public Delete(id: number) {
    return this.http.delete("api/Room/" + id);
  }
  
  
  public ById(id: number) {
    return this.http.get<Room>("api/Room/ById/" + id);
  }
  
  
  public AllFromPlace(placeId: number) {
    return this.http.get<Room[]>("api/Room/FromPlace/" + placeId);
  }
  
  
  public ByName(name: string) {
    return this.http.get<Room>("api/Room/ByName/" + name);
  }
}
