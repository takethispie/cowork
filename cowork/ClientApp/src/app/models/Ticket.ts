import {TicketState} from "./TicketState";
import {DateTime, Duration} from 'luxon';
import {User} from './User';
import {Ware} from './Ware';
import {Place} from './Place';
import {TicketComment} from './TicketComment';

export  class Ticket {
    public Id: number;
    public OpenedById: number;
    public State: TicketState;
    public Description: string;
    public PlaceId: number;
    public OpendedBy: User;
    public Ware: Ware;
    public AttributedTo: User;
    public Place: Place;
    public Created: DateTime;
    public Title: string;
    public Comments: TicketComment[];

    public constructor() {
        this.Comments = [];
    }
}
