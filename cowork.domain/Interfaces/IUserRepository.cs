using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface IUserRepository {

        List<User> GetAll();

        User GetById(long id);

        List<User> GetAllWithPaging(int page, int amount);

        long Update(User user);

        bool DeleteById(long userId);

        long Create(User user);

    }

}