import {UserType} from './UserType';

export class User {
    public Id: number;
    public FirstName: string;
    public LastName: string;
    public IsAStudent: boolean;
    public Token: string;
    public Type: UserType;

    constructor() {
        this.Id = -1;
        this.FirstName = "";
        this.LastName = "";
        this.IsAStudent = false;
        this.Type = UserType.User;
    }
}
