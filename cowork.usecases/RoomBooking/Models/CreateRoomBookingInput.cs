using System;

namespace cowork.usecases.RoomBooking.Models {

    public class CreateRoomBookingInput {

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long RoomId { get; set; }
        public long ClientId { get; set; }

    }

}