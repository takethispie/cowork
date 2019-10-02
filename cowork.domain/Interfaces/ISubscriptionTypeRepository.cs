using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface ISubscriptionTypeRepository {

        List<SubscriptionType> GetAll();

        SubscriptionType GetById(long id);
        
        bool Delete(long id);

        long Create(SubscriptionType type);

        long Update(SubscriptionType type);

        List<SubscriptionType> GetAllWithPaging(int page, int amount);

    }

}