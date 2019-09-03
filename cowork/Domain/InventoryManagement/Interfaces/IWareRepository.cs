using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface IWareRepository {

        List<Ware> GetAll();

        List<Ware> GetAllFromPlace(long id);

        List<Ware> GetAllFromPlaceWithPaging(long id, int amount, int page);

        Ware GetById(long id);

        bool Delete(long id);

        long Update(Ware ware);

        long Create(Ware ware);
    }

}