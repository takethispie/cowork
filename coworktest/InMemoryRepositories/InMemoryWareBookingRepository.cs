using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryWareBookingRepository : IWareBookingRepository {

        public List<WareBooking> WareBookings;


        public InMemoryWareBookingRepository() {
            WareBookings = new List<WareBooking>();
        }
        
        public long Create(WareBooking wareBooking) {
            var id = WareBookings.Count;
            wareBooking.Id = id;
            WareBookings.Add(wareBooking);
            return id;
        }


        public bool Delete(long id) {
            var item = WareBookings.Find(b => b.Id == id);
            if (item == null) return false;
            WareBookings.Remove(item);
            return true;
        }


        public long Update(WareBooking item) {
            long id = -1;
            WareBookings = WareBookings.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public List<WareBooking> GetAll() {
            return WareBookings;
        }


        public WareBooking GetById(long id) {
            return WareBookings.Find(w => w.Id == id);
        }


        public List<WareBooking> GetByUser(long userId) {
            return WareBookings.FindAll(w => w.UserId == userId);
        }


        public List<WareBooking> GetStartingAt(DateTime dateTime) {
            return WareBookings.FindAll(w => w.Start >= dateTime);
        }


        public List<WareBooking> GetAllByWareId(long id) {
            return WareBookings.FindAll(w => w.WareId == id);
        }


        public List<WareBooking> GetAllByWareIdStartingAt(long id, DateTime dateTime) {
            return WareBookings.FindAll(w => w.WareId == id && w.Start >= dateTime);
        }


        public List<WareBooking> GetAllFromDate(DateTime dateTime) {
            return WareBookings.FindAll(w => w.Start >= dateTime);
        }


        public List<WareBooking> GetWithPaging(int page, int size, DateTime startingAt) {
            return WareBookings.Where(w => w.Start >= startingAt).Skip(page * size).Take(size).ToList();
        }


        public List<WareBooking> GetWithPaging(int page, int size) {
            return WareBookings.Skip(page * size).Take(size).ToList();
        }

    }

}