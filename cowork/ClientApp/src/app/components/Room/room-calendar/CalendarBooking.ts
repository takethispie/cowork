import {Room} from '../../../models/Room';
import {User} from '../../../models/User';
import {RoomBooking} from '../../../models/RoomBooking';
import {colors} from './colors';
import {DateTime} from 'luxon';

export class CalendarBooking {
    public id: number;
    public title: string;
    public color: { primary: string, secondary: string };
    public start: Date;
    public end: Date;
    public draggable: boolean;
    public resizable: { beforeStart: boolean, afterEnd: boolean };
    public RoomId: number;
    public ClientId: number;
    public Room: Room;
    public Client: User;

    public ToRoomBooking(): RoomBooking {
        const roomBooking = new RoomBooking();
        roomBooking.Id = this.id;
        roomBooking.Room = this.Room;
        roomBooking.RoomId = this.RoomId;
        roomBooking.ClientId = this.ClientId;
        roomBooking.Client = this.Client;
        roomBooking.Start = DateTime.fromJSDate(this.start);
        roomBooking.End = DateTime.fromJSDate(this.end);
        return roomBooking;
    }

    public static FromRoomBooking(roomBooking: RoomBooking): CalendarBooking {
        const calBooking: CalendarBooking = new CalendarBooking();
        calBooking.Client = roomBooking.Client;
        calBooking.ClientId = roomBooking.ClientId;
        calBooking.color = colors.yellow;
        calBooking.Room = roomBooking.Room;
        calBooking.RoomId = roomBooking.RoomId;
        calBooking.start = roomBooking.Start.toJSDate();
        calBooking.end = roomBooking.End.toJSDate();
        calBooking.id = roomBooking.Id;
        return calBooking;
    }
}
