import {Ware} from '../../../models/Ware';
import {User} from '../../../models/User';
import {WareBooking} from '../../../models/WareBooking';
import {DateTime} from 'luxon';
import {colors} from './colors';

export class CalendarBooking {
    public id: number;
    public title: string;
    public color: { primary: string, secondary: string };
    public start: Date;
    public end: Date;
    public draggable: boolean;
    public resizable: { beforeStart: boolean, afterEnd: boolean };
    public WareId: number;
    public UserId: number;
    public Ware: Ware;
    public User: User;

    public ToWareBooking(): WareBooking {
        const wareBooking = new WareBooking();
        wareBooking.Id = this.id;
        wareBooking.Ware = this.Ware;
        wareBooking.WareId = this.WareId;
        wareBooking.UserId = this.UserId;
        wareBooking.User = this.User;
        wareBooking.Start = DateTime.fromJSDate(this.start);
        wareBooking.End = DateTime.fromJSDate(this.end);
        return wareBooking;
    }

    public static FromWareBooking(wareBooking: WareBooking): CalendarBooking {
        const calBooking: CalendarBooking = new CalendarBooking();
        calBooking.User = wareBooking.User;
        calBooking.UserId = wareBooking.UserId;
        calBooking.color = colors.yellow;
        calBooking.Ware = wareBooking.Ware;
        calBooking.WareId = wareBooking.WareId;
        calBooking.start = wareBooking.Start.toJSDate();
        calBooking.end = wareBooking.End.toJSDate();
        calBooking.id = wareBooking.Id;
        return calBooking;
    }
}
