using System;

namespace cowork.usecases.Meal.Models {

    public class MealInput {

        public MealInput(DateTime date, string description, long placeId) {
            Date = date;
            Description = description;
            PlaceId = placeId;
        }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }

    }

}