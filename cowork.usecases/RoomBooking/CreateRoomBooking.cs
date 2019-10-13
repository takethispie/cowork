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
            if (Input.Start.Day != Input.End.Date.Day) return -1;
            
            var placeId = roomRepository.GetById(Input.RoomId).PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == Input.Start.DayOfWeek);
            if (Input.Start.Hour < openings.StartHour || new TimeSpan(0, Input.End.Hour, Input.End.Minute, 0) 
                > new TimeSpan(0, openings.EndHour, openings.EndMinutes, 0))
                throw new Exception("Erreur: Impossible de réserver du matériel hors des heures d'ouvertures");
            var otherSlots = roomBookingRepository.GetAllFromGivenDate(Input.Start.Date);
            if (otherSlots != null) {
                var overlapping = otherSlots.Where(slot => slot.End >= Input.End && slot.Start <= Input.Start)
                    .Any(slot => Input.RoomId == slot.RoomId || slot.ClientId == Input.ClientId);
                if (overlapping) return -1;
            }
            var roomBooking = new domain.RoomBooking(Input.Start, Input.End, Input.RoomId, Input.ClientId);
            return roomBookingRepository.Create(roomBooking);
        }
    }

}