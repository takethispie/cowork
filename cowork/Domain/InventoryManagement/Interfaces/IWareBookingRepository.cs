using System;
using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface IWareBookingRepository {

        long Create(WareBooking wareBooking);

        bool Delete(long id);

        long Update(WareBooking wareBooking);

        List<WareBooking> GetAll();

        WareBooking GetById(long id);

        List<WareBooking> GetByUser(long userId);

        List<WareBooking> GetStartingAt(DateTime dateTime);

        List<WareBooking> GetAllByWareId(long id);
        
        List<WareBooking> GetAllByWareIdStartingAt(long id, DateTime dateTime);

        List<WareBooking> GetAllFromDate(DateTime dateTime);

        List<WareBooking> GetWithPaging(int page, int size, DateTime startingAt);


    }

}