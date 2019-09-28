using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface ISubscriptionRepository {

        List<Subscription> GetAll();
        List<Subscription> GetAllWithPaging(int page, int amount);

        Subscription GetById(long id);

        Subscription GetOfUser(long userId);

        long Update(Subscription sub);

        bool Delete(long subId);

        long Create(Subscription sub);

    }

}