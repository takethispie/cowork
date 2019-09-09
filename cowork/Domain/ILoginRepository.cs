namespace coworkdomain {

    public interface ILoginRepository {

        long Create(Login login);

        bool Delete(long id);

        long Auth(string email, string password);

    }

}