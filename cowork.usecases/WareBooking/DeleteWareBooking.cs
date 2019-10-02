using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class DeleteWareBooking : IUseCase<bool> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly long Id;

        public DeleteWareBooking(IWareBookingRepository wareBookingRepository, long id) {
            this.wareBookingRepository = wareBookingRepository;
            Id = id;
        }


        public bool Execute() {
            return wareBookingRepository.Delete(Id);
        }

    }

}