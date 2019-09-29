using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetBookingsOfRoom : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        private readonly long RoomId;

        public GetBookingsOfRoom(IRoomBookingRepository roomBookingRepository, long roomId) {
            this.roomBookingRepository = roomBookingRepository;
            RoomId = roomId;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAllOfRoom(RoomId);
        }

    }

}