using System;
using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class SubscriptionRepository : ISubscriptionRepository {

        private SqlDataMapper<Subscription> dataMapper;
        private const string innerJoin = " INNER JOIN \"SubscriptionType\" ST on \"Subscription\".\"TypeId\" = ST.\"Id\" INNER JOIN \"Place\" P on \"Subscription\".\"PlaceId\" = P.\"Id\" INNER JOIN \"Users\" U on \"Subscription\".\"UserId\" = U.\"Id\" ";


        public SubscriptionRepository(string connection) {
            dataMapper = new SqlDataMapper<Subscription>(SqlDbType.Postgresql, connection, new SubscriptionBuilder());
        }


        public List<Subscription> GetAll() {
            var sql = "SELECT * FROM public.\"Subscription\"" + innerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public Subscription GetById(long id) {
            var sql = "SELECT * FROM public.\"Subscription\"" + innerJoin + "WHERE \"Subscription\".\"Id\"= @p;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public Subscription GetOfUser(long userId) {
            var sql = "SELECT * FROM public.\"Subscription\"" + innerJoin + "WHERE \"Subscription\".\"UserId\"= @p;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", userId)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public long Update(Subscription sub) {
            const string sql = "UPDATE public.\"Subscription\" SET \"Id\"= @id, \"TypeId\"= @typeId, \"LatestRenewal\"= @latestRenewal, \"UserId\"= @userId, \"PlaceId\"= @placeId, \"FixedContract\"= @fixedContract WHERE \"Subscription\".\"Id\"= @id RETURNING \"Id\";";
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
            const string sql = "INSERT INTO public.\"Subscription\"(\"Id\", \"TypeId\", \"LatestRenewal\", \"UserId\", \"PlaceId\", \"FixedContract\") VALUES (DEFAULT, @typeId, @latestRenewal, @userId, @placeId, @fixedContract) RETURNING \"Id\";";
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