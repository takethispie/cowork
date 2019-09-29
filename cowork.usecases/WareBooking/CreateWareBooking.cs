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
            var existing = wareBookingRepository.GetStartingAt(Input.Start.Date)
                .Where(booking => booking.WareId == Input.WareId)
                .Any(slot => slot.End >= Input.End && slot.Start <= Input.Start);
            if (existing) throw new Exception("Erreur: créneau déjà pris");
            var placeId = wareRepository.GetById(Input.WareId).PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == Input.Start.DayOfWeek);
            if (Input.Start.Hour < openings.StartHour && Input.Start.Minute < openings.StartMinutes
                || Input.End.Hour < openings.EndHour && Input.End.Minute < openings.EndMinutes)
                throw new Exception("Erreur: Impossible de réserver du matériel hors des heures d'ouvertures");
            var wareBooking = new domain.WareBooking(Input.UserId, Input.WareId, Input.Start, Input.End);
            return wareBookingRepository.Create(wareBooking);
        }

    }

}