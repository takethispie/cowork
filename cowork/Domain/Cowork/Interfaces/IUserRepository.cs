using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IUserRepository {

        List<User> GetAll();

        User GetById(long id);
        
        long Update(User user);

        bool DeleteById(long userId);
        
        long Create(User user);
    }

}