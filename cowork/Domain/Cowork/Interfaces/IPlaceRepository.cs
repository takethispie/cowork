using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IPlaceRepository {

        List<Place> GetAll();

        Place GetById(long id);

        Place GetByName(string name);

        long Update(Place place);

        bool DeleteById(long id);

        bool DeleteByName(string name);

        long Create(Place place);

    }

}