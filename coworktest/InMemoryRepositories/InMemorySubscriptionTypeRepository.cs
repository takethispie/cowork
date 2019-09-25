using System.Collections.Generic;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemorySubscriptionTypeRepository : ISubscriptionTypeRepository {

        public List<SubscriptionType> SubscriptionTypes;


        public InMemorySubscriptionTypeRepository() {
            SubscriptionTypes = new List<SubscriptionType>();
        }
        
        public List<SubscriptionType> GetAll() {
            throw new System.NotImplementedException();
        }


        public SubscriptionType GetById(long id) {
            throw new System.NotImplementedException();
        }


        public SubscriptionType GetByName(string name) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Create(SubscriptionType type) {
            throw new System.NotImplementedException();
        }


        public long Update(SubscriptionType type) {
            throw new System.NotImplementedException();
        }


        public List<SubscriptionType> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }

    }

}