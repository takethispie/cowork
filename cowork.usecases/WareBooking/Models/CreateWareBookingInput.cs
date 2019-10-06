using System;

namespace cowork.usecases.WareBooking.Models {

    public class CreateWareBookingInput {
        

        public CreateWareBookingInput(long userId, long wareId, DateTime start, DateTime end) {
            UserId = userId;
            WareId = wareId;
            Start = start;
            End = end;
        }

        public long UserId { get; }
        public long WareId { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

    }

}