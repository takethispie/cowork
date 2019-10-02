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
            return wareBookingRepository.Update(WareBooking);
        }

    }

}