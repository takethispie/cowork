import {User} from './User';

export class TicketComment {
    public Id: number;
    public TicketId: number;
    public AuthorId: number;
    public Author: User;
    public Content: string;
    public Created: string;
}
