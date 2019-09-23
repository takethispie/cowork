using System.Collections.Generic;
using System.Linq;
using coworkdomain;
using coworkpersistence;

namespace coworktest.InMemoryRepositories {

    public class InMemoryLoginRepository : ILoginRepository {

        private List<Login> logins;


        public InMemoryLoginRepository() {
            logins = new List<Login>();
        }

        public long Create(Login login) {
            var id = -1;
            if (logins.Count == 0) id = 0;
            else id = logins.Count;
            login.Id = id;
            logins.Add(login);
            return id;
        }


        public bool Delete(long id) {
            logins.RemoveAt((int)id);
            return true;
        }


        public long Update(Login login) {
            long id = -1;
            logins = logins.Select(l => {
                if (l.Id == login.Id) {
                    id = login.Id;
                    return login;
                }
                id = -1;
                return l;
            }).ToList();
            return id;
        }


        public long Auth(string email, string password) {
            var res = logins.FirstOrDefault(l =>
                l.Email == email && PasswordHashing.VerifyPasswordHash(password, l.PasswordHash, l.PasswordSalt));
            if (res == null) return -1;
            return res.Id;
        }


        public Login ById(long id) => logins.FirstOrDefault(l => l.Id == id);


        public Login ByUserId(long id) => logins.FirstOrDefault(l => l.UserId == id);


        public List<Login> WithPaging(int page, int amount) => logins.Skip(page * amount).Take(amount).ToList();

    }

}