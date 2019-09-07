import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Ware} from "../models/Ware";
import {CONTENTJSON} from "../Utils";

@Injectable({
    providedIn: 'root'
})
export class WareService {

    constructor(public http: HttpClient) { }

    public All(): Observable<Ware[]> {
        return this.http.get<Ware[]>("api/ware", CONTENTJSON);
    }


    public AllWithPaging(page: number, amount: number) {
        return this.http.get<Ware[]>("api/Ware/WithPaging/" + page + "/" + amount)
    }


    public Create(ware: Ware): Observable<number> {
        return this.http.post<number>("api/ware", ware, CONTENTJSON);
    }


    public Update(ware: Ware): Observable<number> {
        return this.http.put<number>("api/ware", ware, CONTENTJSON);
    }


    public Delete(id: number) {
        return this.http.delete("api/ware/" + id);
    }

    public ById(id: number): Observable<Ware> {
        return this.http.get<Ware>("api/ware/" + id);
    }

    public AllFromPlace(placeId: number): Observable<Ware[]> {
        return this.http.get<Ware[]>("api/ware/FromPlace/" + placeId);
    }

    public AllFromPlaceWithPaging(placeId: number, amount: number, page: number) {
        return this.http.get<Ware[]>("api/ware/FromPlaceWithPaging/" + placeId + "/" + amount + "/" + page);
    }
}
