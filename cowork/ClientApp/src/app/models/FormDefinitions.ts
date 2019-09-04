import {Field} from "../components/dynamic-form-builder/Field";
import {Table} from "./Table";
import {DateTime} from "luxon";

export const FormDefinitions: {[key in Table]: Field[]} = {
    Login: [
        
    ],
    Meal: [
        { Type: "DatePicker", Name: "Date", Label: "Date", Value: DateTime.local().toISODate()},
        { Type: "Text", Name: "Description", Label: "Description", Value: ""},
    ],
    MealBooking: [
        
    ],
    Place: [

    ],
    Room: [

    ],
    RoomBooking: [

    ],
    StaffLocation: [

    ],
    Subscription: [

    ],
    SubscriptionType: [

    ],
    Ticket: [

    ],
    TicketAttribution: [

    ],
    TicketComment: [

    ],
    TicketWare: [

    ],
    TimeSlot: [

    ],
    Ware: [

    ],
    WareBooking: [

    ],
    User: [
        
    ]
}; 