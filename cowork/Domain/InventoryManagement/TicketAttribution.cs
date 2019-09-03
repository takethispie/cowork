namespace coworkdomain.InventoryManagement {

    public class TicketAttribution {

        public TicketAttribution(long id, long ticketId, long staffId) {
            Id = id;
            TicketId = ticketId;
            StaffId = staffId;
        }


        public TicketAttribution() { }

        public long Id { get; set; }
        public long TicketId { get; set; }
        public long StaffId { get; set; }

    }

}