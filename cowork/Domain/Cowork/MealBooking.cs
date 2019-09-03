namespace coworkdomain.Cowork {

    public class MealBooking {

        public MealBooking() { }

        public MealBooking(long id, long mealId, long userId, string note) {
            Id = id;
            MealId = mealId;
            UserId = userId;
            Note = note;
        }

        public long Id { get; set; }
        public long MealId { get; set; }
        public long UserId { get; set; }
        public string Note { get; set; }
        
        public Meal Meal { get; set; }
        public User User { get; set; }

    }

}