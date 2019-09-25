using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryWareRepository : IWareRepository {

        public List<Ware> Wares;


        public InMemoryWareRepository() {
            Wares = new List<Ware>();
        }
        
        public List<Ware> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<Ware> GetAllFromPlace(long id) {
            throw new System.NotImplementedException();
        }


        public List<Ware> GetAllFromPlaceWithPaging(long id, int amount, int page) {
            throw new System.NotImplementedException();
        }


        public List<Ware> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public Ware GetById(long id) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(Ware ware) {
            throw new System.NotImplementedException();
        }


        public long Create(Ware ware) {
            throw new System.NotImplementedException();
        }

    }

}