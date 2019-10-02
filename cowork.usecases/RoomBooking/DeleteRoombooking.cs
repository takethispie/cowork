using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class DeleteRoombooking : IUseCase<bool> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly long Id;

        public DeleteRoombooking(IRoomBookingRepository roomBookingRepository, long id) {
            this.roomBookingRepository = roomBookingRepository;
            Id = id;
        }


        public bool Execute() {
            return roomBookingRepository.Delete(Id);
        }

    }

}