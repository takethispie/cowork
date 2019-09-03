using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IUserTypeRepository {

        long Create(UserType userType);

        bool Delete(long id);

        long Update(UserType userType);

        List<UserType> GetAll();

        UserType GetById(long id);

        UserType GetByName(string name);
    }

}