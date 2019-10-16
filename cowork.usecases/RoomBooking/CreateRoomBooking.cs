using System;
using System.Linq;
using cowork.domain.Interfaces;
using cowork.usecases.RoomBooking.Models;

namespace cowork.usecases.RoomBooking {

    public class CreateRoomBooking : IUseCase<long> {

        private readonly IRoomBookingRepository roomBookingRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IRoomRepository roomRepository;
        public readonly CreateRoomBookingInput Input;

        public CreateRoomBooking(IRoomBookingRepository roomBookingRepository, ITimeSlotRepository timeSlotRepository, 
                                 IRoomRepository roomRepository, CreateRoomBookingInput input) {
            this.roomBookingRepository = roomBookingRepository;
            this.timeSlotRepository = timeSlotRepository;
            Input = input;
            this.roomRepository = roomRepository;
        }


        public long Execute() {
            if (Input.Start.Day != Input.End.Date.Day || Input.Start.Date < DateTime.Today) return -1;
            var placeId = roomRepository.GetById(Input.RoomId).PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == Input.Start.DayOfWeek);
            if (openings == null) return -1;
            if (Input.Start.Hour < openings.StartHour || new TimeSpan(0, Input.End.Hour, Input.End.Minute, 0) 
                > new TimeSpan(0, openings.EndHour, openings.EndMinutes, 0))
                throw new Exception("Erreur: Impossible de réserver une salle hors des heures d'ouvertures");
            var date = new DateTime(Input.Start.Year, Input.Start.Month, Input.Start.Day);
            var otherSlots = roomBookingRepository.GetAllFromGivenDate(date);
            if (otherSlots != null) {
                var noOverlap = otherSlots.Where(slot => Input.RoomId == slot.RoomId || slot.ClientId == Input.ClientId)
                    .All((slot => slot.End <= Input.Start || slot.Start >= Input.End));
                if (!noOverlap) return -1;
            }
            var roomBooking = new domain.RoomBooking(Input.Start, Input.End, Input.RoomId, Input.ClientId);
            return roomBookingRepository.Create(roomBooking);
        }
    }

}