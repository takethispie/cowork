using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetRoomBookingsOfUser : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly long UserId;

        public GetRoomBookingsOfUser(IRoomBookingRepository roomBookingRepository, long userId) {
            this.roomBookingRepository = roomBookingRepository;
            UserId = userId;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAllOfUser(UserId);
        }

    }

}