using System;
using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class RoomBookingRepository : IRoomBookingRepository {

        private const string InnerJoin =
            " INNER JOIN \"Users\" U on \"RoomBooking\".\"UserId\" = U.\"Id\" INNER JOIN \"Room\" R on \"RoomBooking\".\"RoomId\" = R.\"Id\" INNER JOIN \"Place\" P on R.\"PlaceId\" = P.\"Id\" ";

        private readonly SqlDataMapper<RoomBooking> datamapper;


        public RoomBookingRepository(string connection) {
            datamapper = new SqlDataMapper<RoomBooking>(SqlDbType.Postgresql, connection, new RoomBookingBuilder());
        }


        public List<RoomBooking> GetAll() {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin + ";";
            return datamapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public RoomBooking GetById(long id) {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin +
                               "WHERE \"RoomBooking\".\"Id\"= @id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public List<RoomBooking> GetAllOfUser(long userId) {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin + "WHERE \"UserId\"= @id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", userId)
            };
            return datamapper.MultiItemCommand(sql, parameters);
        }


        public List<RoomBooking> GetAllOfRoom(long roomId) {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin + "WHERE \"RoomId\"= @id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", roomId)
            };
            return datamapper.MultiItemCommand(sql, parameters);
        }


        public List<RoomBooking> GetAllOfRoomStartingAtDate(long roomId, DateTime date) {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin +
                               "WHERE \"Start\"::DATE >= @date::DATE AND \"RoomId\"= @id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("date", date),
                new NpgsqlParameter("id", roomId)
            };
            return datamapper.MultiItemCommand(sql, parameters);
        }


        public List<RoomBooking> GetAllFromGivenDate(DateTime date) {
            const string sql = "SELECT * FROM public.\"RoomBooking\"" + InnerJoin +
                               "WHERE \"Start\"::DATE= @date::DATE;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("date", date)
            };
            return datamapper.MultiItemCommand(sql, parameters);
        }


        public List<RoomBooking> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"RoomBooking\"" + InnerJoin +
                               " ORDER BY \"RoomBooking\".\"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public long Update(RoomBooking reservation) {
            const string sql =
                "UPDATE public.\"RoomBooking\" SET \"RoomId\"= @roomId, \"UserId\"= @userId, \"Start\"= @start, \"End\"= @enddate WHERE \"Id\"= @id RETURNING  \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", reservation.Id),
                new NpgsqlParameter("roomId", reservation.RoomId),
                new NpgsqlParameter("userId", reservation.ClientId),
                new NpgsqlParameter("start", reservation.Start),
                new NpgsqlParameter("enddate", reservation.End)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }


        public bool Delete(long reservationId) {
            const string sql = "DELETE FROM public.\"RoomBooking\" WHERE \"Id\"= @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", reservationId)
            };
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(RoomBooking reservation) {
            const string sql =
                "INSERT INTO public.\"RoomBooking\"(\"Id\", \"RoomId\", \"UserId\", \"Start\", \"End\") VALUES (DEFAULT, @roomId, @userId, @start, @enddate) RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("roomId", reservation.RoomId),
                new NpgsqlParameter("userId", reservation.ClientId),
                new NpgsqlParameter("start", reservation.Start),
                new NpgsqlParameter("enddate", reservation.End)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }

    }

}