using System;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.WareBooking.Models;

namespace cowork.usecases.WareBooking {

    public class CreateWareBooking : IUseCase<long> {

        private readonly IWareBookingRepository wareBookingRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IWareRepository wareRepository;
        public readonly CreateWareBookingInput Input;

        public CreateWareBooking(IWareBookingRepository wareBookingRepository, ITimeSlotRepository timeSlotRepository, 
                                 IWareRepository wareRepository, CreateWareBookingInput input) {
            this.wareBookingRepository = wareBookingRepository;
            Input = input;
            this.timeSlotRepository = timeSlotRepository;
            this.wareRepository = wareRepository;
        }


        public long Execute() {
            if (Input.Start.Day != Input.End.Day || Input.Start.Date < DateTime.Today) return -1;
            var noConflict = wareBookingRepository.GetStartingAt(Input.Start.Date)
                .Where(booking => booking.WareId == Input.WareId)
                .All(slot => slot.End <= Input.Start || slot.Start >= Input.End);
            if (!noConflict) throw new Exception("Erreur: créneau déjà pris");
            var ware = wareRepository.GetById(Input.WareId);
            var placeId = ware.PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == Input.Start.DayOfWeek);
            if (Input.Start.Hour < openings.StartHour || new TimeSpan(0, Input.End.Hour, Input.End.Minute, 0) 
                > new TimeSpan(0, openings.EndHour, openings.EndMinutes, 0))
                throw new Exception("Erreur: Impossible de réserver du matériel hors des heures d'ouvertures");
            var wareBooking = new domain.WareBooking(Input.UserId, Input.WareId, Input.Start, Input.End);
            return wareBookingRepository.Create(wareBooking);
        }

    }

}