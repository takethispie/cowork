import {DateTime} from 'luxon';

export const CONTENTJSON = {
    headers: {
        'Content-Type': 'application/json'
    }
};

export const tz = Intl.DateTimeFormat().resolvedOptions().timeZone;

export const BookingToUtc = <T>(model: T & { Start: DateTime, End: DateTime}) => {
    model.End = model.End.toUTC();
    model.Start = model.Start.toUTC();
    return model;
}


export const ParseDateTime = (prop: DateTime | string) => {
    prop = DateTime.fromISO(prop as unknown as string, { zone: "utc" });
    return prop;
}