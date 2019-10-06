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
        let list = new List<TimeSlot>(slots);
        if(list.Count() === 0) return list.ToArray(); 
        return list.Where(sl => sl.Day != 0).OrderBy(slot => slot.Day).ToArray();
    }
}
