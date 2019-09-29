using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.RoomBooking {

    public class GetBookingsOfRoomStartingAt : IUseCase<IEnumerable<domain.RoomBooking>> {

        private readonly IRoomBookingRepository roomBookingRepository;
        public readonly long RoomId;
        public readonly DateTime DateTime;

        public GetBookingsOfRoomStartingAt(IRoomBookingRepository roomBookingRepository, long roomId, DateTime dateTime) {
            this.roomBookingRepository = roomBookingRepository;
            RoomId = roomId;
            DateTime = dateTime;
        }


        public IEnumerable<domain.RoomBooking> Execute() {
            return roomBookingRepository.GetAllOfRoomStartingAtDate(RoomId, DateTime);
        }

    }

}