import {DateTime} from "luxon";
import {Place} from './Place';
import {User} from './User';
import {SubscriptionType} from './SubscriptionType';

export class Subscription {
    public Id: number;
    public TypeId: number;
    public ClientId: number;
    public PlaceId: number;
    public LatestRenewal: DateTime;
    public FixedContract: boolean;
    public Place: Place;
    public Client: User;
    public Type: SubscriptionType;

    constructor(id = -1, typeId = null, clientId = null, placeId = null, latesRenewal = DateTime.local(), fixedContract = false, place = null, client = null, type = null) {
        this.Id = id;
        this.TypeId = typeId;
        this.ClientId = clientId;
        this.PlaceId = placeId;
        this.LatestRenewal = latesRenewal;
        this.FixedContract = fixedContract;
        this.Place = place;
        this.Client = client;
        this.Type = type;
    }
}
