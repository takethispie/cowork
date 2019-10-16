using System;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class UpdateWareBooking : IUseCase<long> {

        private readonly IWareBookingRepository wareBookingRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IWareRepository wareRepository;
        public readonly domain.WareBooking WareBooking;

        public UpdateWareBooking(IWareBookingRepository wareBookingRepository, ITimeSlotRepository timeSlotRepository, 
                                 IWareRepository wareRepository, domain.WareBooking wareBooking) {
            this.wareBookingRepository = wareBookingRepository;
            this.timeSlotRepository = timeSlotRepository;
            this.wareRepository = wareRepository;
            WareBooking = wareBooking;
        }


        public long Execute() {
            if (WareBooking.Start.Day != WareBooking.End.Date.Day || WareBooking.Start.Date < DateTime.Today) return -1;
            var ware = wareRepository.GetById(WareBooking.WareId);
            var placeId = ware.PlaceId;
            var openings = timeSlotRepository.GetAllOfPlace(placeId)
                .Find(op => op.Day == WareBooking.Start.DayOfWeek);
            if (openings == null) return -1;
            if (WareBooking.Start.Hour < openings.StartHour || new TimeSpan(0, 
                    WareBooking.End.Hour, WareBooking.End.Minute, 0) 
                > new TimeSpan(0, openings.EndHour, openings.EndMinutes, 0))
                throw new Exception("Erreur: Impossible de réserver du matériel hors des heures d'ouvertures");
            var possibleConflicts = wareBookingRepository.GetAllFromDate(WareBooking.Start.Date).Where(rb => rb.Id != WareBooking.Id).ToList();
            var hasNoConflict = possibleConflicts.All(booking =>
                booking.End <= WareBooking.Start || booking.Start >= WareBooking.End);
            if (!hasNoConflict) throw new Exception("Une réservation est déjà présente pour ces horaires");
            return wareBookingRepository.Update(WareBooking);
        }

    }

}