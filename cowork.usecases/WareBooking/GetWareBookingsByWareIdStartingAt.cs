using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingsByWareIdStartingAt : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly DateTime DateTime;
        public readonly long Id;

        public GetWareBookingsByWareIdStartingAt(IWareBookingRepository wareBookingRepository, long id, DateTime dateTime) {
            this.wareBookingRepository = wareBookingRepository;
            Id = id;
            DateTime = dateTime;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetAllByWareIdStartingAt(Id, DateTime);
        }

    }

}