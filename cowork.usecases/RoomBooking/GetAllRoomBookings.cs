using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetAllRoomBookings : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        private readonly IRoomRepository roomRepoitory;


        public GetAllRoomBookings(IRoomBookingRepository roomBookingRepository) {
            this.roomBookingRepository = roomBookingRepository;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAll();
        }

    }

}