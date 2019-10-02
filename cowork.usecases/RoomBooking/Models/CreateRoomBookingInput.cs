using System;

namespace cowork.usecases.RoomBooking.Models {

    public class CreateRoomBookingInput {

        public CreateRoomBookingInput() { }

        public CreateRoomBookingInput(DateTime start, DateTime end, long roomId, long clientId) {
            Start = start;
            End = end;
            RoomId = roomId;
            ClientId = clientId;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long RoomId { get; set; }
        public long ClientId { get; set; }

    }

}