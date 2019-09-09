using System;

namespace coworkdomain.Cowork {

    public class RoomBooking {

        public RoomBooking() { }


        public RoomBooking(long id, DateTime start, DateTime end, long roomId, long clientId) {
            Id = id;
            Start = start;
            End = end;
            RoomId = roomId;
            ClientId = clientId;
        }


        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long RoomId { get; set; }
        public long ClientId { get; set; }

        public User Client { get; set; }
        public Room Room { get; set; }

    }

}