using System;
using coworkdomain.Cowork;

namespace coworkdomain.InventoryManagement {

    public class WareBooking {

        public WareBooking() { }


        public WareBooking(long id, long userId, long wareId, DateTime start, DateTime end) {
            Id = id;
            UserId = userId;
            WareId = wareId;
            Start = start;
            End = end;
        }


        public long Id { get; set; }
        public long UserId { get; set; }
        public long WareId { get; set; }
        public User User { get; set; }
        public Ware Ware { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }

}