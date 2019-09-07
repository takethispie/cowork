using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface ISubscriptionTypeRepository {

        List<SubscriptionType> GetAll();

        SubscriptionType GetById(long id);

        SubscriptionType GetByName(string name);

        bool Delete(long id);

        long Create(SubscriptionType type);

        long Update(SubscriptionType type);

        List<SubscriptionType> GetAllWithPaging(int page, int amount);

    }

}