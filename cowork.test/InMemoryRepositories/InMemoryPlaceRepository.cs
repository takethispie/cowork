using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemoryPlaceRepository : IPlaceRepository {

        public List<Place> Places;


        public InMemoryPlaceRepository() {
            Places = new List<Place>();
        }
        
        public List<Place> GetAll() {
            return Places;
        }


        public List<Place> GetAllWithPaging(int page, int amount) {
            return Places.Skip(page * amount).Take(amount).ToList();
        }


        public Place GetById(long id) {
            return Places.Find(p => p.Id == id);
        }


        public Place GetByName(string name) {
            return Places.Find(p => p.Name == name);
        }


        public long Update(Place item) {
            long id = -1;
            Places = Places.Select(i => {
                if (i.Id != item.Id) return i;
                id = item.Id;
                return item;
            }).ToList();
            return id;
        }


        public bool Delete(long id) {
            var item = Places.Find(p => p.Id == id);
            if (item == null) return false;
            Places.Remove(item);
            return true;
        }


        public long Create(Place place) {
            var id = Places.Count;
            place.Id = id;
            Places.Add(place);
            return id;
        }

    }

}