import {DateTime} from 'luxon';
import {User} from './User';
import {Ware} from './Ware';

export class WareBooking {
    public Id: number;
    public UserId: number;
    public WareId: number;
    public Start: DateTime;
    public End: DateTime;
    public User: User;
    public Ware: Ware;
}
