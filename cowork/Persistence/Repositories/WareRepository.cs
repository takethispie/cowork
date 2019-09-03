using System.Collections.Generic;
using System.Data.Common;
using Bogus.DataSets;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class WareRepository : IWareRepository {

        private SqlDataMapper<Ware> dataMapper;
        private const string innerJoin = " INNER JOIN \"Place\" P on \"Ware\".\"PlaceId\" = P.\"Id\" ";


        public WareRepository(string connection) {
            dataMapper = new SqlDataMapper<Ware>(SqlDbType.Postgresql, connection, new WareBuilder());
        }
        
        public List<Ware> GetAll() {
            const string sql = "SELECT * FROM public.\"Ware\"" + innerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<Ware> GetAllFromPlace(long id) {
            const string sql = "SELECT * FROM public.\"Ware\"" + innerJoin + "WHERE \"Ware\".\"PlaceId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Ware> GetAllFromPlaceWithPaging(long id, int amount, int page) {
            const string sql = "SELECT * FROM public.\"Ware\"" + innerJoin + "WHERE \"Ware\".\"PlaceId\"= @id ORDER BY \"Ware\".\"Id\" LIMIT @amount OFFSET @skip";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id),
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", amount * page)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public Ware GetById(long id) {
            const string sql = "SELECT * FROM public.\"Ware\"" + innerJoin + "WHERE \"Ware\".\"Id\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM public.\"Ware\" WHERE \"Ware\".\"Id\"= @id RETURNING \"Ware\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(Ware ware) {
            const string sql = "UPDATE public.\"Ware\" SET \"Id\"= @id, \"Name\"= @name, \"Description\"= @description, \"SerialNumber\"= @serialNumber, \"PlaceId\"= @placeId, \"InStorage\"= @inStorage WHERE \"Id\"= @id RETURNING \"Ware\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ware.Id),
                new NpgsqlParameter("name", ware.Name),
                new NpgsqlParameter("description", ware.Description),
                new NpgsqlParameter("serialNumber", ware.SerialNumber),
                new NpgsqlParameter("placeId", ware.PlaceId),
                new NpgsqlParameter("inStorage", ware.InStorage)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public long Create(Ware ware) {
            const string sql = "INSERT INTO public.\"Ware\"(\"Id\", \"Name\", \"Description\", \"SerialNumber\", \"PlaceId\", \"InStorage\")VALUES (DEFAULT, @name, @description, @serialNumber, @placeId, @inStorage) RETURNING \"Ware\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("name", ware.Name),
                new NpgsqlParameter("description", ware.Description),
                new NpgsqlParameter("serialNumber", ware.SerialNumber),
                new NpgsqlParameter("placeId", ware.PlaceId),
                new NpgsqlParameter("inStorage", ware.InStorage)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }

    }

}