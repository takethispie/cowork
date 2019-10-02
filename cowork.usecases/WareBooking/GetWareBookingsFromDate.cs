using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingsFromDate : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly DateTime DateTime;

        public GetWareBookingsFromDate(IWareBookingRepository wareBookingRepository, DateTime dateTime) {
            this.wareBookingRepository = wareBookingRepository;
            DateTime = dateTime;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return wareBookingRepository.GetAllFromDate(DateTime);
        }

    }

}