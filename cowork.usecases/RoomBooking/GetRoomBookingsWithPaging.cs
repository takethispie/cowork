using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetRoomBookingsWithPaging : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetRoomBookingsWithPaging(IRoomBookingRepository roomBookingRepository, int page, int amount) {
            this.roomBookingRepository = roomBookingRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAllWithPaging(Page, Amount);
        }

    }

}