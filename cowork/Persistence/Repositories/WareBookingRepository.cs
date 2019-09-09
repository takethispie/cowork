using System;
using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class WareBookingRepository : IWareBookingRepository {

        private readonly SqlDataMapper<WareBooking> dataMapper;
        private readonly string innerJoin;


        public WareBookingRepository(string conn) {
            dataMapper = new SqlDataMapper<WareBooking>(SqlDbType.Postgresql, conn, new WareBookingBuilder());
            innerJoin =
                " inner join \"Users\" U on \"WareBooking\".\"UserId\" = U.\"Id\" inner join \"Ware\" W on \"WareBooking\".\"WareId\" = W.\"Id\" inner join \"Place\" P on W.\"PlaceId\" = P.\"Id\" ";
        }


        public long Create(WareBooking wareBooking) {
            const string sql =
                "INSERT INTO public.\"WareBooking\"(\"Id\", \"WareId\", \"UserId\", \"Start\", \"End\") VALUES (DEFAULT, @wareId, @userId, @startDate, @endDate) returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("wareId", wareBooking.WareId),
                new NpgsqlParameter("userId", wareBooking.UserId),
                new NpgsqlParameter("startDate", wareBooking.Start),
                new NpgsqlParameter("endDate", wareBooking.End)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"WareBooking\" where \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(WareBooking wareBooking) {
            const string sql =
                "UPDATE public.\"WareBooking\" SET \"Id\"= @id, \"WareId\"= @wareId, \"UserId\"= @userId, \"Start\"= @startDate, \"End\"= @endDate WHERE \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", wareBooking.Id),
                new NpgsqlParameter("wareId", wareBooking.WareId),
                new NpgsqlParameter("userId", wareBooking.UserId),
                new NpgsqlParameter("startDate", wareBooking.Start),
                new NpgsqlParameter("endDate", wareBooking.End)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public List<WareBooking> GetAll() {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + ";";
            return dataMapper.MultiItemCommand(sql, null);
        }


        public WareBooking GetById(long id) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + "WHERE \"Id\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<WareBooking> GetByUser(long userId) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + "WHERE \"UserId\"= @userId";
            var par = new List<DbParameter> {
                new NpgsqlParameter("userId", userId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetStartingAt(DateTime dateTime) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + "WHERE \"Start\"::date >= @startDate";
            var par = new List<DbParameter> {
                new NpgsqlParameter("startDate", dateTime.TimeOfDay)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetAllByWareId(long id) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + "WHERE \"WareId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetAllByWareIdStartingAt(long id, DateTime dateTime) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin +
                      "WHERE \"WareId\"= @id AND \"Start\"::DATE >= @startDate;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("startDate", dateTime.Date),
                new NpgsqlParameter("id", id)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetAllFromDate(DateTime dateTime) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin + "WHERE \"Start\"::date = @startDate";
            var par = new List<DbParameter> {
                new NpgsqlParameter("startDate", dateTime.Date)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetWithPaging(int page, int size, DateTime startingAt) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin +
                      "WHERE \"Start\"::date >= @startDate ORDER BY \"WareBooking\".\"Start\" ASC LIMIT @amount OFFSET @skip";
            var par = new List<DbParameter> {
                new NpgsqlParameter("startDate", startingAt.TimeOfDay),
                new NpgsqlParameter("amount", size),
                new NpgsqlParameter("skip", page * size)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<WareBooking> GetWithPaging(int page, int size) {
            var sql = "SELECT * FROM \"WareBooking\"" + innerJoin +
                      " ORDER BY \"WareBooking\".\"Start\" ASC LIMIT @amount OFFSET @skip";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", size),
                new NpgsqlParameter("skip", page * size)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }

    }

}