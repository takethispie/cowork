using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.WareBooking {

    public class GetWareBookingsWithPaging : IUseCase<IEnumerable<domain.WareBooking>> {

        private readonly IWareBookingRepository wareBookingRepository;
        public readonly int Page;
        public readonly int Amount;
        public readonly DateTime? StartingAt;

        public GetWareBookingsWithPaging(IWareBookingRepository wareBookingRepository, int page, int amount, DateTime? startingAt) {
            this.wareBookingRepository = wareBookingRepository;
            Page = page;
            Amount = amount;
            StartingAt = startingAt;
        }


        public IEnumerable<domain.WareBooking> Execute() {
            return !StartingAt.HasValue 
                ? wareBookingRepository.GetWithPaging(Page, Amount) 
                : wareBookingRepository.GetWithPaging(Page, Amount, StartingAt.Value);
        }

    }

}