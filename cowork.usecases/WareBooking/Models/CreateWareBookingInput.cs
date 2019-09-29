using System;

namespace cowork.usecases.WareBooking.Models {

    public class CreateWareBookingInput {

        public long UserId { get; set; }
        public long WareId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }

}