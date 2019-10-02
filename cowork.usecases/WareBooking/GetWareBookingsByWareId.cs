using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingsByWareId : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly long Id;

        public GetWareBookingsByWareId(IWareBookingRepository wareBookingRepository, long id) {
            this.wareBookingRepository = wareBookingRepository;
            Id = id;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetAllByWareId(Id);
        }

    }

}