import {Place} from './Place';

export class Ware {
    public Id: number;
    public Name: string;
    public Description: string;
    public SerialNumber: string;
    public PlaceId: number;
    public InStorage: boolean;
    public Place: Place;
}
