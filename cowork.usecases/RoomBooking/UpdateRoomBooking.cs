using System;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class UpdateRoomBooking : IUseCase<long> {

        private readonly IRoomBookingRepository roomBookingRepository;
        private readonly domain.RoomBooking roomBooking;

        public UpdateRoomBooking(IRoomBookingRepository roomBookingRepository, domain.RoomBooking roomBooking) {
            this.roomBookingRepository = roomBookingRepository;
            this.roomBooking = roomBooking;
        }


        public long Execute() {
            if (roomBooking.Start.Day != roomBooking.End.Date.Day) return -1;
            var possibleConflicts = roomBookingRepository.GetAllFromGivenDate(roomBooking.Start.Date)
                .Where(rb => rb.Id != roomBooking.Id).ToList();
            var hasNoConflict = possibleConflicts.All(booking =>
                booking.End <= roomBooking.Start || booking.Start >= roomBooking.End);
            if (!hasNoConflict) throw new Exception("Une réservation est déjà présente pour ces horaires");
            return roomBookingRepository.Update(roomBooking);
        }

    }

}