using System.Collections.Generic;
using System.Linq;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryStaffLocationRepository : IStaffLocationRepository {

        public List<StaffLocation> StaffLocations;


        public InMemoryStaffLocationRepository() {
            StaffLocations = new List<StaffLocation>();
        }
        
        public long Create(StaffLocation staffLocation) {
            var id = StaffLocations.Count;
            staffLocation.Id = id;
            StaffLocations.Add(staffLocation);
            return id;
        }


        public bool Delete(long id) {
            var item = StaffLocations.Find(r => r.Id == id);
            if (item == null) return false;
            StaffLocations.Remove(item);
            return true;
        }


        public long Update(StaffLocation item) {
            long id = -1;
            StaffLocations = StaffLocations.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public StaffLocation GetById(long id) {
            return StaffLocations.Find(s => s.Id == id);
        }


        public List<StaffLocation> GetAll() {
            return StaffLocations;
        }


        public List<StaffLocation> GetAllByPlace(long placeId) {
            return StaffLocations.Where(s => s.PlaceId == placeId).ToList();
        }


        public List<StaffLocation> GetAllWithPaging(int page, int amount) {
            return StaffLocations.Skip(page * amount).Take(amount).ToList();
        }

    }

}