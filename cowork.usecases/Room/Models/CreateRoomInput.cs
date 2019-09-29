using cowork.domain;

namespace cowork.usecases.Room.Models {

    public class CreateRoomInput {

        public long PlaceId { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }

    }

}