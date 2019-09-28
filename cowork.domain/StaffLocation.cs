namespace cowork.domain {

    public class StaffLocation {

        public StaffLocation() { }


        public StaffLocation(long id, long userId, long placeId) {
            Id = id;
            UserId = userId;
            PlaceId = placeId;
        }


        public long Id { get; set; }
        public long UserId { get; set; }
        public long PlaceId { get; set; }

    }

}