using System;

namespace cowork.domain {

    public class Meal {

        public Meal() { }


        public Meal(long id, DateTime date, string description, long placeId) {
            Id = id;
            Date = date;
            Description = description;
            PlaceId = placeId;
        }
        
        public Meal(DateTime date, string description, long placeId) {
            Id = -1;
            Date = date;
            Description = description;
            PlaceId = placeId;
        }


        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }

    }

}