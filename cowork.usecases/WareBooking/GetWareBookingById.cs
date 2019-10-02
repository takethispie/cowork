using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingById : IUseCase<domain.WareBooking> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly long Id;

        public GetWareBookingById(IWareBookingRepository wareBookingRepository, long id) {
            this.wareBookingRepository = wareBookingRepository;
            Id = id;
        }


        public domain.WareBooking Execute() {
            return wareBookingRepository.GetById(Id);
        }

    }

}