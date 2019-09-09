using System;
using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IMealBookingRepository {

        List<MealBooking> GetAll();

        List<MealBooking> GetAllFromUser(long userId);

        List<MealBooking> GetAllFromDateAndPlace(DateTime date, long placeId);

        List<MealBooking> GetAllWithPaging(int page, int amount, bool byDateAscending);

        MealBooking GetById(long id);

        bool Delete(long id);

        long Update(MealBooking meal);

        long Create(MealBooking meal);

    }

}