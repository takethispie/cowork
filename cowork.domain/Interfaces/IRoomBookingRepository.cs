using System;
using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface IRoomBookingRepository {

        List<RoomBooking> GetAll();

        RoomBooking GetById(long id);

        List<RoomBooking> GetAllOfUser(long userId);

        List<RoomBooking> GetAllOfRoom(long roomId);

        List<RoomBooking> GetAllOfRoomStartingAtDate(long roomId, DateTime date);

        List<RoomBooking> GetAllFromGivenDate(DateTime date);
        List<RoomBooking> GetAllWithPaging(int page, int amount);

        long Update(RoomBooking reservation);

        bool Delete(long reservationId);

        long Create(RoomBooking reservation);

    }

}