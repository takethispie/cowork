using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class SubscriptionRepository : ISubscriptionRepository {

        private const string InnerJoin =
            " INNER JOIN \"SubscriptionType\" ST on \"Subscription\".\"TypeId\" = ST.\"Id\" INNER JOIN \"Place\" P on \"Subscription\".\"PlaceId\" = P.\"Id\" INNER JOIN \"Users\" U on \"Subscription\".\"UserId\" = U.\"Id\" ";

        private readonly SqlDataMapper<Subscription> dataMapper;


        public SubscriptionRepository(string connection) {
            dataMapper = new SqlDataMapper<Subscription>(SqlDbType.Postgresql, connection, new SubscriptionBuilder());
        }


        public List<Subscription> GetAll() {
            var sql = "SELECT * FROM public.\"Subscription\"" + InnerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<Subscription> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"Subscription\"" + InnerJoin +
                               " ORDER BY \"Subscription\".\"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public Subscription GetById(long id) {
            var sql = "SELECT * FROM public.\"Subscription\"" + InnerJoin + "WHERE \"Subscription\".\"Id\"= @p;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public Subscription GetOfUser(long userId) {
            var sql = "SELECT * FROM public.\"Subscription\"" + InnerJoin + "WHERE \"Subscription\".\"UserId\"= @p;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", userId)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public long Update(Subscription sub) {
            const string sql =
                "UPDATE public.\"Subscription\" SET \"TypeId\"= @typeId, \"LatestRenewal\"= @latestRenewal, \"UserId\"= @userId, \"PlaceId\"= @placeId, \"FixedContract\"= @fixedContract WHERE \"Subscription\".\"Id\"= @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", sub.Id),
                new NpgsqlParameter("typeId", sub.TypeId),
                new NpgsqlParameter("latestRenewal", sub.LatestRenewal),
                new NpgsqlParameter("userId", sub.ClientId),
                new NpgsqlParameter("placeId", sub.PlaceId),
                new NpgsqlParameter("fixedContract", sub.FixedContract)
            };
            return dataMapper.NoQueryCommand(sql, parameters);
        }


        public bool Delete(long subId) {
            const string sql = "DELETE FROM public.\"Subscription\" WHERE \"Id\"= @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", subId)
            };
            return dataMapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(Subscription sub) {
            const string sql =
                "INSERT INTO public.\"Subscription\"(\"Id\", \"TypeId\", \"LatestRenewal\", \"UserId\", \"PlaceId\", \"FixedContract\") VALUES (DEFAULT, @typeId, @latestRenewal, @userId, @placeId, @fixedContract) RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("typeId", sub.TypeId),
                new NpgsqlParameter("latestRenewal", sub.LatestRenewal),
                new NpgsqlParameter("userId", sub.ClientId),
                new NpgsqlParameter("placeId", sub.PlaceId),
                new NpgsqlParameter("fixedContract", sub.FixedContract)
            };
            return dataMapper.NoQueryCommand(sql, parameters);
        }

    }

}