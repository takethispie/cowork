using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryStaffLocationRepository : IStaffLocationRepository {

        public List<StaffLocation> StaffLocations;


        public InMemoryStaffLocationRepository() {
            StaffLocations = new List<StaffLocation>();
        }
        
        public long Create(StaffLocation staffLocation) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(StaffLocation staffLocation) {
            throw new System.NotImplementedException();
        }


        public StaffLocation GetById(long id) {
            throw new System.NotImplementedException();
        }


        public List<StaffLocation> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<StaffLocation> GetAllByPlace(long placeId) {
            throw new System.NotImplementedException();
        }


        public List<StaffLocation> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }

    }

}