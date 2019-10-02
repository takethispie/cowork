using System;

namespace cowork.usecases.TimeSlot.Models {

    public class CreateTimeSlotInput {

        public short StartHour { get; set; }
        public short StartMinutes { get; set; }
        public short EndHour { get; set; }
        public short EndMinutes { get; set; }
        public long PlaceId { get; set; }
        public DayOfWeek Day { get; set; }

    }

}