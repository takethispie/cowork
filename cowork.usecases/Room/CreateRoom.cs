using cowork.domain.Interfaces;
using cowork.usecases.Room.Models;

namespace cowork.usecases.Room {

    public class CreateRoom : IUseCase<long> {

        private readonly IRoomRepository roomRepository;
        public readonly CreateRoomInput Input;

        public CreateRoom(IRoomRepository roomRepository, CreateRoomInput input) {
            this.roomRepository = roomRepository;
            Input = input;
        }


        public long Execute() {
            var room = new domain.Room(Input.PlaceId, Input.Name, Input.Type);
            return roomRepository.Create(room);
        }

    }

}