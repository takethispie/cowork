import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Ware} from '../../../models/Ware';
import {WareService} from '../../../services/ware.service';
import {Observable} from 'rxjs';

@Component({
    selector: 'ware-list',
    templateUrl: './ware-list.component.html',
    styleUrls: ['./ware-list.component.scss'],
})
export class WareListComponent implements OnInit {
    public Wares: Ware[];
    @Input() UserId: number;
    @Input() ShowUserRentedWare: Ware[];
    @Input() PlaceId: number;
    @Input() Refresher: Observable<void>;
    @Output() WareSelected: EventEmitter<Ware> = new EventEmitter<Ware>();
    @Output() FirstWare: EventEmitter<Ware> = new EventEmitter<Ware>();

    private page: number = 0;
    private amount: number = 30;


    constructor(public wareService: WareService) {
        this.Wares = [];
    }

    ngOnInit() {
        this.Refresher.subscribe({
           next: value => this.ReloadData()
        });

        if(this.PlaceId == null) return;
        this.wareService.AllFromPlaceWithPaging(this.PlaceId, this.amount, this.page).subscribe(res => {
            if(res.length === 0) return;
            res.forEach(ware => this.Wares.push(ware));
            this.FirstWare.emit(res[0]);
            this.page++;
        });
    }


    ReloadData() {
        this.page = 0;
        this.Wares = [];
        this.LoadData(null);
    }


    LoadData(event) {
        if(this.PlaceId == null) return;
        this.wareService.AllFromPlaceWithPaging(this.PlaceId, this.amount, this.page).subscribe(res => {
            if(res.length === 0) return;
            res.forEach(ware => this.Wares.push(ware));
            this.page++;
            if(event != null) event.target.complete();
        });
    }

    ItemSelected(ware: Ware) {
        this.WareSelected.emit(ware);
    }
}
