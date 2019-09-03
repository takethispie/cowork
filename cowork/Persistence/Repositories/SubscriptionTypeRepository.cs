using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class SubscriptionTypeRepository : ISubscriptionTypeRepository {

        private readonly SqlDataMapper<SubscriptionType> dataMapper;


        public SubscriptionTypeRepository(string connection) {
            dataMapper =
                new SqlDataMapper<SubscriptionType>(SqlDbType.Postgresql, connection, new SubscriptionTypeBuilder());
        }


        public List<SubscriptionType> GetAll() {
            const string sql = "SELECT * FROM public.\"SubscriptionType\";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public SubscriptionType GetById(long id) {
            const string sql = "SELECT * FROM public.\"SubscriptionType\" WHERE \"Id\"= @id;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public SubscriptionType GetByName(string name) {
            const string sql = "SELECT * FROM public.\"SubscriptionType\" WHERE \"Name\"= @name;";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("name", name)
            };
            return dataMapper.OneItemCommand(sql, parameters);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM public.\"SubscriptionType\" WHERE  \"Id\"= @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(SubscriptionType type) {
            const string sql =
                "INSERT INTO public.\"SubscriptionType\"(\"Id\", \"Name\", \"FixedContractDurationMonth\", \"PriceFirstHour\", \"PriceNextHalfHour\", \"PriceDay\", \"PriceDayStudent\", \"FixedContractMonthlyFee\", \"ContractFreeMonthlyFee\", \"Description\") VALUES (DEFAULT, @name, @fixedContractDurationMonth, @priceFirstHour, @priceNextHalfHour, @priceDay, @priceDayStudent, @fixedContractMonthlyFee, @contractFreeMonthlyFee, @description) RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("name", type.Name),
                new NpgsqlParameter("fixedContractDurationMonth", type.FixedContractDurationMonth),
                new NpgsqlParameter("priceFirstHour", type.PriceFirstHour),
                new NpgsqlParameter("priceNextHalfHour", type.PriceNextHalfHour),
                new NpgsqlParameter("priceDay", type.PriceDay),
                new NpgsqlParameter("priceDayStudent", type.PriceDayStudent),
                new NpgsqlParameter("fixedContractMonthlyFee", type.MonthlyFeeFixedContract),
                new NpgsqlParameter("contractFreeMonthlyFee", type.MonthlyFeeContractFree),
                new NpgsqlParameter("description", type.Description)
            };
            return dataMapper.NoQueryCommand(sql, parameters);
        }


        public long Update(SubscriptionType type) {
            const string sql =
                "UPDATE public.\"SubscriptionType\" SET \"Id\"= @id, \"Name\"= @name, \"FixedContractDurationMonth\"= @fixedContractDurationMonth, \"PriceFirstHour\"= @priceFirstHour, \"PriceNextHalfHour\"= @priceNextHalfHour, \"PriceDay\"= @priceDay, \"PriceDayStudent\"= @priceDayStudent, \"FixedContractMonthlyFee\"= @fixedContractMonthlyFee, \"ContractFreeMonthlyFee\"= @contractFreeMonthlyFee, \"Description\"= @description WHERE \"Id\"= @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", type.Id),
                new NpgsqlParameter("name", type.Name),
                new NpgsqlParameter("fixedContractDurationMonth", type.FixedContractDurationMonth),
                new NpgsqlParameter("priceFirstHour", type.PriceFirstHour),
                new NpgsqlParameter("priceNextHalfHour", type.PriceNextHalfHour),
                new NpgsqlParameter("priceDay", type.PriceDay),
                new NpgsqlParameter("priceDayStudent", type.PriceDayStudent),
                new NpgsqlParameter("fixedContractMonthlyFee", type.MonthlyFeeFixedContract),
                new NpgsqlParameter("contractFreeMonthlyFee", type.MonthlyFeeContractFree),
                new NpgsqlParameter("description", type.Description)
            };
            return dataMapper.NoQueryCommand(sql, parameters);
        }

    }

}