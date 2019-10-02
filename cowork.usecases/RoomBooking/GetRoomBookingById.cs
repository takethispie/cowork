using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetRoomBookingById : IUseCase<domain.RoomBooking> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly long RoomId;

        public GetRoomBookingById(IRoomBookingRepository roomBookingRepository, long roomId) {
            this.roomBookingRepository = roomBookingRepository;
            RoomId = roomId;
        }


        public domain.RoomBooking Execute() {
            return roomBookingRepository.GetById(RoomId);
        }

    }

}