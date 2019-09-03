import {RoomType} from "./RoomType";
import {Place} from './Place';

export class Room {
    public Id: number;
    public PlaceId: number;
    public Name: string;
    public Type: RoomType;
    public Place: Place;
}
