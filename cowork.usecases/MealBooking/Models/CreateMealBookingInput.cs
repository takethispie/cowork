namespace cowork.usecases.MealBooking.Models {

    public class CreateMealBookingInput {

        public long MealId { get; set; }
        public long UserId { get; set; }
        public string Note { get; set; }

    }

}