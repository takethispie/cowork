using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingsStartingAtDate : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly DateTime Start;

        public GetWareBookingsStartingAtDate(IWareBookingRepository wareBookingRepository, DateTime start) {
            this.wareBookingRepository = wareBookingRepository;
            Start = start;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetStartingAt(Start);
        }

    }

}