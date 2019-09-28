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

    public class MealRepository : IMealRepository {

        private readonly SqlDataMapper<Meal> dataMapper;


        public MealRepository(string connection) {
            dataMapper = new SqlDataMapper<Meal>(SqlDbType.Postgresql, connection, new MealBuilder());
        }


        public List<Meal> GetAll() {
            const string sql = "SELECT * FROM \"Meal\";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<Meal> GetAllFromPlace(long id) {
            const string sql = "SELECT * FROM \"Meal\" WHERE \"PlaceId\"= @p;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Meal> GetAllFromDateAndPlace(DateTime date, long placeId) {
            const string sql = "SELECT * FROM \"Meal\" WHERE \"Date\"= @date AND \"PlaceId\"= @placeId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("date", date),
                new NpgsqlParameter("placeId", placeId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Meal> GetAllFromPlaceStartingAtDate(long placeId, DateTime date) {
            const string sql = "SELECT * FROM \"Meal\" WHERE \"Date\" >= @date::date AND \"PlaceId\"= @placeId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("date", date),
                new NpgsqlParameter("placeId", placeId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Meal> GetAllByPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"Meal\" ORDER BY \"Meal\".\"Date\" DESC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public Meal GetById(long id) {
            const string sql = "SELECT * FROM \"Meal\" WHERE \"Id\"= @p;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"Meal\" WHERE \"Id\"= @p RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(Meal meal) {
            const string sql =
                "UPDATE public.\"Meal\" SET \"Date\"= @date, \"Description\"= @description, \"PlaceId\"= @placeId WHERE \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", meal.Id),
                new NpgsqlParameter("date", meal.Date),
                new NpgsqlParameter("description", meal.Description),
                new NpgsqlParameter("placeId", meal.PlaceId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public long Create(Meal meal) {
            const string sql =
                "INSERT INTO public.\"Meal\"(\"Id\", \"Date\", \"Description\", \"PlaceId\")VALUES (DEFAULT, @date, @description, @placeId) RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("date", meal.Date),
                new NpgsqlParameter("description", meal.Description),
                new NpgsqlParameter("placeId", meal.PlaceId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }

    }

}