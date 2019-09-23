using System.Collections.Generic;
using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryUserRepository : IUserRepository {

        private List<User> users;


        public InMemoryUserRepository() {
            users = new List<User>();
        }
        
        public List<User> GetAll() => users;


        public List<User> GetAllWithPaging(int page, int amount) => users.Skip(page * amount).Take(amount).ToList();


        public User GetById(long id) => users.FirstOrDefault(s => s.Id == id);


        public long Update(User sub) {
            long id = -1;
            users = users.Select(u => {
                if (u.Id == sub.Id) {
                    id = sub.Id;
                    return sub;
                }
                id = -1;
                return u;
            }).ToList();
            return id;
        }


        public bool DeleteById(long id) {
            users.RemoveAt((int)id);
            return true;
        }


        public long Create(User user) {
            var id = -1;
            if (users.Count == 0) id = 0;
            else id = users.Count;
            user.Id = id;
            users.Add(user);
            return id;
        }

    }

}