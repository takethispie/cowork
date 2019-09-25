using System;
using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryWareBookingRepository : IWareBookingRepository {

        public List<WareBooking> WareBookings;


        public InMemoryWareBookingRepository() {
            WareBookings = new List<WareBooking>();
        }
        
        public long Create(WareBooking wareBooking) {
            throw new NotImplementedException();
        }


        public bool Delete(long id) {
            throw new NotImplementedException();
        }


        public long Update(WareBooking wareBooking) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetAll() {
            throw new NotImplementedException();
        }


        public WareBooking GetById(long id) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetByUser(long userId) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetStartingAt(DateTime dateTime) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetAllByWareId(long id) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetAllByWareIdStartingAt(long id, DateTime dateTime) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetAllFromDate(DateTime dateTime) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetWithPaging(int page, int size, DateTime startingAt) {
            throw new NotImplementedException();
        }


        public List<WareBooking> GetWithPaging(int page, int size) {
            throw new NotImplementedException();
        }

    }

}