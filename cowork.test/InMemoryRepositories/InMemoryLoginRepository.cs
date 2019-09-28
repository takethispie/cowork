using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemoryLoginRepository : ILoginRepository {

        public List<Login> Logins;


        public InMemoryLoginRepository() {
            Logins = new List<Login>();
        }

        public long Create(Login login) {
            var id = Logins.Count;
            login.Id = id;
            Logins.Add(login);
            return id;
        }


        public bool Delete(long id) {
            Logins.RemoveAt((int)id);
            return true;
        }


        public long Update(Login login) {
            long id = -1;
            Logins = Logins.Select(l => {
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
            var res = Logins.FirstOrDefault(l =>
                l.Email == email && PasswordHashing.VerifyPasswordHash(password, l.PasswordHash, l.PasswordSalt));
            if (res == null) return -1;
            return res.Id;
        }


        public Login ById(long id) => Logins.FirstOrDefault(l => l.Id == id);


        public Login ByUserId(long id) => Logins.FirstOrDefault(l => l.UserId == id);


        public List<Login> WithPaging(int page, int amount) => Logins.Skip(page * amount).Take(amount).ToList();

    }

}