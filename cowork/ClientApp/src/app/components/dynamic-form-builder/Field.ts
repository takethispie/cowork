export class Field {
    public Type: "Text" | "Radio" | "Checkbox";
    public Name: string;
    public Label: string;
    public Value: string;
    public Options?: {
        Name: string,
        Label: string,
        Value: string
    }[];
}