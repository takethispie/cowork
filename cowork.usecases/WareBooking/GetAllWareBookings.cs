using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetAllWareBookings : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;

        public GetAllWareBookings(IWareBookingRepository wareBookingRepository) {
            this.wareBookingRepository = wareBookingRepository;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetAll();
        }

    }

}