using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryWareRepository : IWareRepository {

        public List<Ware> Wares;


        public InMemoryWareRepository() {
            Wares = new List<Ware>();
        }
        
        public List<Ware> GetAll() {
            return Wares;
        }


        public List<Ware> GetAllFromPlace(long id) {
            return Wares.FindAll(w => w.PlaceId == id);
        }


        public List<Ware> GetAllFromPlaceWithPaging(long id, int amount, int page) {
            return Wares.FindAll(w => w.PlaceId == id).Skip(amount * page).Take(amount).ToList();
        }


        public List<Ware> GetAllWithPaging(int page, int amount) {
            return Wares.Skip(amount * page).Take(amount).ToList();
        }


        public Ware GetById(long id) {
            return Wares.Find(w => w.Id == id);
        }


        public bool Delete(long id) {
            var item = Wares.Find(b => b.Id == id);
            if (item == null) return false;
            Wares.Remove(item);
            return true;
        }


        public long Update(Ware item) {
            long id = -1;
            Wares = Wares.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public long Create(Ware ware) {
            var id = Wares.Count;
            ware.Id = id;
            Wares.Add(ware);
            return id;
        }

    }

}