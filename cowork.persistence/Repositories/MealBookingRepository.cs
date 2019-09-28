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

    public class MealBookingRepository : IMealBookingRepository {

        private const string InnerJoin =
            " INNER JOIN \"Meal\" M on \"MealReservation\".\"MealId\" = M.\"Id\" INNER JOIN \"Users\" U on \"MealReservation\".\"UserId\" = U.\"Id\" ";

        private readonly SqlDataMapper<MealBooking> datamapper;


        public MealBookingRepository(string connection) {
            datamapper = new SqlDataMapper<MealBooking>(SqlDbType.Postgresql, connection, new MealBookingBuilder());
        }


        public List<MealBooking> GetAll() {
            const string sql = "SELECT * FROM \"MealReservation\"" + InnerJoin + ";";
            return datamapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<MealBooking> GetAllFromUser(long userId) {
            const string sql = "SELECT * FROM \"MealReservation\"" + InnerJoin +
                               "WHERE \"MealReservation\".\"UserId\"= @userId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("userId", userId)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public List<MealBooking> GetAllFromDateAndPlace(DateTime date, long placeId) {
            const string sql =
                "SELECT * FROM \"MealReservation\"" + InnerJoin + "WHERE \"Date\"= @date AND \"PlaceId\"= @placeId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("date", date),
                new NpgsqlParameter("placeId", placeId)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public List<MealBooking> GetAllWithPaging(int page, int amount, bool byDateAscending) {
            const string sql = "SELECT * FROM \"MealReservation\"" + InnerJoin +
                               "ORDER BY \"Meal\".\"Date\" @order LIMIT @amount OFFSET @skip;";

            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", amount * page),
                new NpgsqlParameter("order", byDateAscending ? "ASC" : "DESC")
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public MealBooking GetById(long id) {
            const string sql = "SELECT * FROM \"MealReservation\"" + InnerJoin +
                               "WHERE \"MealReservation\".\"Id\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.OneItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql =
                "DELETE FROM \"MealReservation\" WHERE \"MealReservation\".\"Id\"= @id RETURNING \"MealReservation\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(MealBooking meal) {
            const string sql =
                "UPDATE public.\"MealReservation\" SET \"MealId\"= @mealId, \"UserId\"= @userId, \"Note\"= @note WHERE \"Id\"= @id RETURNING \"MealReservation\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", meal.Id),
                new NpgsqlParameter("mealId", meal.MealId),
                new NpgsqlParameter("userId", meal.UserId),
                new NpgsqlParameter("note", meal.Note)
            };
            return datamapper.NoQueryCommand(sql, par);
        }


        public long Create(MealBooking meal) {
            const string sql =
                "INSERT INTO public.\"MealReservation\"(\"Id\", \"MealId\", \"UserId\", \"Note\")VALUES (DEFAULT, @mealId, @userId, @note) RETURNING \"MealReservation\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("mealId", meal.MealId),
                new NpgsqlParameter("userId", meal.UserId),
                new NpgsqlParameter("note", meal.Note)
            };
            return datamapper.NoQueryCommand(sql, par);
        }

    }

}