import {WeekDay} from '@angular/common';
import {Place} from './Place';
import {List} from 'linqts';

export class TimeSlot {
    public Id: number;
    public StartHour: number;
    public StartMinutes: number;
    public EndHour: number;
    public EndMinutes: number;
    public PlaceId: number;
    public Place: Place;
    public Day: WeekDay;
    
    public static OrderByDay(slots: TimeSlot[]) {
        const list = new List<TimeSlot>(slots).OrderBy(slot => slot.Day);
        const first = list.First();
        list.Remove(list.First());
        list.Add(first);
        return list.ToArray();
    }
}
