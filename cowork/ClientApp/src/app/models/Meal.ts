import {DateTime} from "luxon";

export class Meal {
    public Id: number;
    public Date: DateTime;
    public Description: string;
    public PlaceId: number;
}