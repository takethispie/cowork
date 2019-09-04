using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IPlaceRepository {

        List<Place> GetAll();
        List<Place> GetAllWithPaging(int page, int amount);

        Place GetById(long id);

        Place GetByName(string name);

        long Update(Place place);

        bool DeleteById(long id);

        bool DeleteByName(string name);

        long Create(Place place);

    }

}