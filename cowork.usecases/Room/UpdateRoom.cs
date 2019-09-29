using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class UpdateRoom : IUseCase<long> {

        private readonly IRoomRepository roomRepository;
        public readonly domain.Room Room;

        public UpdateRoom(IRoomRepository roomRepository, domain.Room room) {
            this.roomRepository = roomRepository;
            Room = room;
        }


        public long Execute() {
            return roomRepository.Update(Room);
        }

    }

}