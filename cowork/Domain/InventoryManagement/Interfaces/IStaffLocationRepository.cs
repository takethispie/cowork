using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface IStaffLocationRepository {

        long Create(StaffLocation staffLocation);

        bool Delete(long id);

        long Update(StaffLocation staffLocation);

        StaffLocation GetById(long id);

        List<StaffLocation> GetAll();

        List<StaffLocation> GetAllByPlace(long placeId);
        List<StaffLocation> GetAllWithPaging(int page, int amount);

    }

}