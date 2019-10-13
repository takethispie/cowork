using System;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class UpdateRoomBooking : IUseCase<long> {

        private readonly IRoomBookingRepository roomBookingRepository;
        private readonly IRoomRepository roomRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly domain.RoomBooking roomBooking;

        public UpdateRoomBooking(IRoomBookingRepository roomBookingRepository, IRoomRepository roomRepository, 
                                 ITimeSlotRepository timeSlotRepository, domain.RoomBooking roomBooking) {
            this.roomBookingRepository = roomBookingRepository;
            this.roomBooking = roomBooking;
            this.roomRepository = roomRepository;
            this.timeSlotRepository = timeSlotRepository;
        }


        public long Execute() {
            if (roomBooking.Start.Day != roomBooking.End.Date.Day) return -1;
            var placeId = roomRepository.GetById(roomBooking.RoomId).PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == roomBooking.Start.DayOfWeek);
            if (roomBooking.Start.Hour < openings.StartHour || new TimeSpan(0, roomBooking.End.Hour, roomBooking.End.Minute, 0) 
                > new TimeSpan(0, openings.EndHour, openings.EndMinutes, 0))
                throw new Exception("Erreur: Impossible de réserver du matériel hors des heures d'ouvertures");
            var possibleConflicts = roomBookingRepository.GetAllFromGivenDate(roomBooking.Start.Date)
                .Where(rb => rb.Id != roomBooking.Id).ToList();
            var hasNoConflict = possibleConflicts.All(booking =>
                booking.End >= roomBooking.End && booking.Start <= roomBooking.Start);
            if (!hasNoConflict) throw new Exception("Une réservation est déjà présente pour ces horaires");
            return roomBookingRepository.Update(roomBooking);
        }

    }

}