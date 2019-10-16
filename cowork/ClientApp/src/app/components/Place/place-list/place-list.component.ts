import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Place} from '../../../models/Place';
import {TimeSlot} from '../../../models/TimeSlot';
import {TimeSlotService} from '../../../services/time-slot.service';

@Component({
  selector: 'place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.scss'],
})
export class PlaceListComponent implements OnInit {

  @Input() Places: Place[];
  @Input() SelectedId: number;
  @Output() PlaceChosen: EventEmitter<Place> = new EventEmitter();

  constructor(public timeSlotService: TimeSlotService) {}

  Choose(item: Place) {
    if(this.SelectedId === item.Id) this.PlaceChosen.emit(null);
    else this.PlaceChosen.emit(item);
  }

  ngOnInit(): void {
  }

  public GetWeekDayName(id: number) {
    switch (id) {
      case 0: return "Dimanche";
      case 1: return "Lundi";
      case 2: return "Mardi";
      case 3: return "Mercredi";
      case 4: return "Jeudi";
      case 5: return "Vendredi";
      case 6: return "Samedi";
    }
  }

  OrderByDay(openedTimes: TimeSlot[]) {
    if(openedTimes == null || openedTimes.length === 0) return [];
    return TimeSlot.OrderByDay(openedTimes).map(ts => this.timeSlotService.TimeSlotFromUtc(ts));
  }
}
