using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemorySubscriptionTypeRepository : ISubscriptionTypeRepository {

        public List<SubscriptionType> SubscriptionTypes;


        public InMemorySubscriptionTypeRepository() {
            SubscriptionTypes = new List<SubscriptionType>();
        }
        
        public List<SubscriptionType> GetAll() {
            return SubscriptionTypes;
        }


        public SubscriptionType GetById(long id) {
            return SubscriptionTypes.Find(s => s.Id == id);
        }


        public SubscriptionType GetByName(string name) {
            return SubscriptionTypes.Find(s => s.Name == name);
        }


        public bool Delete(long id) {
            var item = SubscriptionTypes.Find(b => b.Id == id);
            if (item == null) return false;
            SubscriptionTypes.Remove(item);
            return true;
        }


        public long Create(SubscriptionType type) {
            var id = SubscriptionTypes.Count;
            type.Id = id;
            SubscriptionTypes.Add(type);
            return id;
        }


        public long Update(SubscriptionType item) {
            long id = -1;
            SubscriptionTypes = SubscriptionTypes.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public List<SubscriptionType> GetAllWithPaging(int page, int amount) {
            return SubscriptionTypes.Skip(page * amount).Take(amount).ToList();
        }

    }

}