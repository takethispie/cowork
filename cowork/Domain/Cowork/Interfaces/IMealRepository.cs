using System;
using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IMealRepository {

        List<Meal> GetAll();

        List<Meal> GetAllFromPlace(long id);

        List<Meal> GetAllFromDateAndPlace(DateTime date, long placeId);

        List<Meal> GetAllFromPlaceStartingAtDate(long placeId, DateTime date);
        List<Meal> GetAllByPaging(int page, int amount);

        Meal GetById(long id);

        bool Delete(long id);

        long Update(Meal meal);

        long Create(Meal meal);

    }

}