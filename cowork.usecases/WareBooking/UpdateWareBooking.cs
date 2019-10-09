using System;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class UpdateWareBooking : IUseCase<long> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly domain.WareBooking WareBooking;

        public UpdateWareBooking(IWareBookingRepository wareBookingRepository, domain.WareBooking wareBooking) {
            this.wareBookingRepository = wareBookingRepository;
            WareBooking = wareBooking;
        }


        public long Execute() {
            if (WareBooking.Start.Day != WareBooking.End.Date.Day) return -1;
            var possibleConflicts = wareBookingRepository.GetAllFromDate(WareBooking.Start.Date).Where(rb => rb.Id != WareBooking.Id).ToList();
            var hasNoConflict = possibleConflicts.All(booking =>
                booking.End <= WareBooking.Start || booking.Start >= WareBooking.End);
            if (!hasNoConflict) throw new Exception("Une réservation est déjà présente pour ces horaires");
            return wareBookingRepository.Update(WareBooking);
        }

    }

}