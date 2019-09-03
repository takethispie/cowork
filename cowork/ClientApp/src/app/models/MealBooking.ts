import {Meal} from './Meal';
import {User} from './User';

export class MealBooking {
    public Id: number;
    public MealId: number;
    public UserId: number;
    public Note: string;
    public Meal: Meal;
    public User: User;

    constructor(id = -1, mealId = null, userId = null, note = "") {
        this.Id = id;
        this.MealId = mealId;
        this.UserId = userId;
        this.Note = note;
    }
}
