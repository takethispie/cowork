import {DateTime} from 'luxon';

export enum FieldType {
    Text,
    Select,
    CheckBox,
    DatePicker,
    DateTimePicker,
    TimePicker,
    ReadonlyText,
    Number,
    ReadonlyNumber,
}

export interface FieldOptions {
    Label: string,
    Value: string | number | boolean
}

export class Field {
    constructor(public Type: FieldType, public Name: string, public Label: string, public Value: string | number | boolean, public Options?: FieldOptions[]) {

    }

    SetValueToDefault() {
        switch(this.Type) {
            case FieldType.Text:
            case FieldType.ReadonlyText:
                this.Value =  "";
                break;

            case FieldType.Select:
                this.Value = 0;
                break;

            case FieldType.CheckBox:
                this.Value = false;
                break;

            case FieldType.DatePicker:
                this.Value = DateTime.local().toISODate();
                break;

            case FieldType.DateTimePicker:
                this.Value = DateTime.local().toISO();
                break;

            case FieldType.TimePicker:
                this.Value = DateTime.local().toISO();
                break;

            case FieldType.Number:
            case FieldType.ReadonlyNumber:
                this.Value = -1;
                break;

        }
    }
}