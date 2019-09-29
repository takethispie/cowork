using cowork.domain.Interfaces;

namespace cowork.usecases.Room {

    public class DeleteRoom : IUseCase<bool> {

        private readonly IRoomRepository roomRepository;
        public readonly long Id;

        public DeleteRoom(IRoomRepository roomRepository, long id) {
            this.roomRepository = roomRepository;
            Id = id;
        }


        public bool Execute() {
            return roomRepository.Delete(Id);
        }

    }

}