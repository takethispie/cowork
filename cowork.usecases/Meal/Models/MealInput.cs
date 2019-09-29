using System;

namespace cowork.usecases.Meal.Models {

    public class MealInput {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }

    }

}