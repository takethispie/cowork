using System;

namespace coworkdomain.Cowork {

    public class TimeSlot {

        public TimeSlot(long id, DayOfWeek day, short startHour, short startMinutes, short endHour, short endMinutes, long placeId) {
            Id = id;
            Day = day;
            StartHour = startHour;
            StartMinutes = startMinutes;
            EndHour = endHour;
            EndMinutes = endMinutes;
            PlaceId = placeId;
        }
        
        public  TimeSlot() {}

        public long Id { get; set; }
        public short StartHour { get; set; }
        public short StartMinutes { get; set; }
        public short EndHour { get; set; }
        public short EndMinutes { get; set; }
        public long PlaceId { get; set; }
        public Place Place { get; set; }
        public DayOfWeek Day { get; set; }

    }

}