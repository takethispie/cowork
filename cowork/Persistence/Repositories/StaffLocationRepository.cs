using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class StaffLocationRepository : IStaffLocationRepository {

        private SqlDataMapper<StaffLocation> dataMapper;


        public StaffLocationRepository(string conn) {
            dataMapper = new SqlDataMapper<StaffLocation>(SqlDbType.Postgresql, conn, new StaffLocationBuilder());
        }

        public long Create(StaffLocation staffLocation) {
            const string sql = "INSERT INTO public.\"StaffLocation\"(\"Id\", \"UserId\", \"PlaceId\") VALUES (DEFAULT, @userId, @placeId) RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("userId", staffLocation.UserId),
                new NpgsqlParameter("placeId", staffLocation.PlaceId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"StaffLocation\" WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(StaffLocation staffLocation) {
            const string sql = "UPDATE public.\"StaffLocation\" SET \"Id\"= @id, \"UserId\"= @userId, \"PlaceId\"= @placeId WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", staffLocation.Id),
                new NpgsqlParameter("userId", staffLocation.UserId),
                new NpgsqlParameter("placeId", staffLocation.PlaceId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public StaffLocation GetById(long id) {
            const string sql = "SELECT * FROM \"StaffLocation\" WHERE \"Id\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<StaffLocation> GetAll() {
            const string sql = "SELECT * FROM \"StaffLocation\";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<StaffLocation> GetAllByPlace(long placeId) {
            const string sql = "SELECT * FROM \"StaffLocation\" WHERE \"PlaceId\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", placeId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<StaffLocation> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"StaffLocation\" ORDER BY \"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }

    }

}