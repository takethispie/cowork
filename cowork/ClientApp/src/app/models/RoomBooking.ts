import {DateTime, Duration} from "luxon";
import {Room} from './Room';
import {User} from './User';

export class RoomBooking {
    public Id: number;
    public Start: DateTime;
    public End: DateTime;
    public RoomId: number;
    public ClientId: number;
    public Room: Room;
    public Client: User;
}
