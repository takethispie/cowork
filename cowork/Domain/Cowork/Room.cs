namespace coworkdomain.Cowork {

    public class Room {

        public Room(long id, long placeId, string name, RoomType type) {
            Id = id;
            PlaceId = placeId;
            Name = name;
            Type = type;
        }


        public Room() { }

        public long Id { get; set; }
        public long PlaceId { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }

        public Place Place { get; set; }

    }

}