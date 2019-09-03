export class Field {
    public Type: "Text" | "Select" | "Checkbox";
    public Name: string;
    public Label: string;
    public Value: string | number | boolean;
    public Options?: {
        Label: string,
        Value: string | number | boolean
    }[];
}