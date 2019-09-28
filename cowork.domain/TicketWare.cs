namespace cowork.domain {

    public class TicketWare {

        public TicketWare() { }


        public TicketWare(long id, long ticketId, long wareId) {
            Id = id;
            TicketId = ticketId;
            WareId = wareId;
        }


        public long Id { get; set; }
        public long TicketId { get; set; }
        public long WareId { get; set; }
        public Ware Ware { get; set; }

    }

}