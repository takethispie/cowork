using coworkdomain.Cowork;

namespace coworkdomain.InventoryManagement {

    public class Ware {

        public Ware(long id, string name, string description, string serialNumber, long placeId, bool inStorage) {
            Id = id;
            Name = name;
            Description = description;
            SerialNumber = serialNumber;
            PlaceId = placeId;
            InStorage = inStorage;
        }


        public Ware() { }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public long PlaceId { get; set; }
        public bool InStorage { get; set; }

        public Place Place { get; set; }

    }

}