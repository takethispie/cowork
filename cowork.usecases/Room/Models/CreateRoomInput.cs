using cowork.domain;

namespace cowork.usecases.Room.Models {

    public class CreateRoomInput {

        public CreateRoomInput(long placeId, string name, RoomType type) {
            PlaceId = placeId;
            Name = name;
            Type = type;
        }
        
        public long PlaceId { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }

    }

}