using System.Collections.Generic;
using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemorySubscriptionRepository : ISubscriptionRepository {

        private List<Subscription> subscriptions;


        public InMemorySubscriptionRepository() {
            subscriptions = new List<Subscription>();
        }

        public List<Subscription> GetAll() => subscriptions;


        public List<Subscription> GetAllWithPaging(int page, int amount) => subscriptions.Skip(page * amount).Take(amount).ToList();


        public Subscription GetById(long id) => subscriptions.FirstOrDefault(s => s.Id == id);


        public Subscription GetOfUser(long userId) => subscriptions.FirstOrDefault(s => s.ClientId == userId);


        public long Update(Subscription sub) {
            long id = -1;
            subscriptions = subscriptions.Select(s => {
                if (s.Id == sub.Id) {
                    id = sub.Id;
                    return sub;
                }
                id = -1;
                return s;
            }).ToList();
            return id;
        }


        public bool Delete(long id) {
            subscriptions.RemoveAt((int)id);
            return true;
        }


        public long Create(Subscription sub) {
            var id = -1;
            if (subscriptions.Count == 0) id = 0;
            else id = subscriptions.Count;
            sub.Id = id;
            subscriptions.Add(sub);
            return id;
        }

    }

}