using System.Collections.Generic;

namespace coworkdomain {

    public interface ILoginRepository {

        long Create(Login login);

        bool Delete(long id);

        long Update(Login login);

        long Auth(string email, string password);

        List<Login> WithPaging(int page, int amount);

    }

}