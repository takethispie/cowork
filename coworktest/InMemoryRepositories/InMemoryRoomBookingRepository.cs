using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryRoomBookingRepository : IRoomBookingRepository {

        public List<RoomBooking> Bookings;


        public InMemoryRoomBookingRepository() {
            Bookings = new List<RoomBooking>();
        }
        
        public List<RoomBooking> GetAll() {
            return Bookings;
        }


        public RoomBooking GetById(long id) {
            return Bookings.Find(b => b.Id == id);
        }


        public List<RoomBooking> GetAllOfUser(long userId) {
            return Bookings.Where(b => b.ClientId == userId).ToList();
        }


        public List<RoomBooking> GetAllOfRoom(long roomId) {
            return Bookings.Where(b => b.RoomId == roomId).ToList();
        }


        public List<RoomBooking> GetAllOfRoomStartingAtDate(long roomId, DateTime date) {
            return Bookings.Where(b => b.RoomId == roomId && b.Start >= date).ToList();
        }


        public List<RoomBooking> GetAllFromGivenDate(DateTime date) {
            return Bookings.Where(b => b.Start == date).ToList();
        }


        public List<RoomBooking> GetAllWithPaging(int page, int amount) {
            return Bookings.Skip(page * amount).Take(amount).ToList();
        }


        public long Update(RoomBooking item) {
            long id = -1;
            Bookings = Bookings.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public bool Delete(long id) {
            var item = Bookings.Find(b => b.Id == id);
            if (item == null) return false;
            Bookings.Remove(item);
            return true;
        }


        public long Create(RoomBooking reservation) {
            var id = Bookings.Count;
            reservation.Id = id;
            Bookings.Add(reservation);
            return id;
        }

    }

}