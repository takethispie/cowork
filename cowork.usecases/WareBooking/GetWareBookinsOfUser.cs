using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookinsOfUser : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly long UserId;

        public GetWareBookinsOfUser(IWareBookingRepository wareBookingRepository, long userId) {
            this.wareBookingRepository = wareBookingRepository;
            UserId = userId;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetByUser(UserId);
        }

    }

}