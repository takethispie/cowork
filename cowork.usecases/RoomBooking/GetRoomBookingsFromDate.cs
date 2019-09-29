using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetRoomBookingsFromDate : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly DateTime DateTime;

        public GetRoomBookingsFromDate(IRoomBookingRepository roomBookingRepository, DateTime dateTime) {
            this.roomBookingRepository = roomBookingRepository;
            DateTime = dateTime;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAllFromGivenDate(DateTime);
        }

    }

}