using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class GetRoomById : IUseCase<domain.Room> {

        private readonly IRoomRepository roomRepository;
        public readonly long Id;

        public GetRoomById(IRoomRepository roomRepository, long id) {
            this.roomRepository = roomRepository;
            Id = id;
        }


        public domain.Room Execute() {
            return roomRepository.GetById(Id);
        }

    }

}