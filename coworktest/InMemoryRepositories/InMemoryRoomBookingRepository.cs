using System;
using System.Collections.Generic;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryRoomBookingRepository : IRoomBookingRepository {

        public List<RoomBooking> Bookings;


        public InMemoryRoomBookingRepository() {
            Bookings = new List<RoomBooking>();
        }
        
        public List<RoomBooking> GetAll() {
            throw new NotImplementedException();
        }


        public RoomBooking GetById(long id) {
            throw new NotImplementedException();
        }


        public List<RoomBooking> GetAllOfUser(long userId) {
            throw new NotImplementedException();
        }


        public List<RoomBooking> GetAllOfRoom(long roomId) {
            throw new NotImplementedException();
        }


        public List<RoomBooking> GetAllOfRoomStartingAtDate(long roomId, DateTime date) {
            throw new NotImplementedException();
        }


        public List<RoomBooking> GetAllFromGivenDate(DateTime date) {
            throw new NotImplementedException();
        }


        public List<RoomBooking> GetAllWithPaging(int page, int amount) {
            throw new NotImplementedException();
        }


        public long Update(RoomBooking reservation) {
            throw new NotImplementedException();
        }


        public bool Delete(long reservationId) {
            throw new NotImplementedException();
        }


        public long Create(RoomBooking reservation) {
            throw new NotImplementedException();
        }

    }

}