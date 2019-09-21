using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class TicketWareRepository : ITicketWareRepository {

        private const string innerJoin =
            " INNER JOIN \"Ware\" W on \"TicketWare\".\"WareId\" = W.\"Id\" inner join \"Place\" P on W.\"PlaceId\" = P.\"Id\" ";

        private readonly SqlDataMapper<TicketWare> dataMapper;


        public TicketWareRepository(string conn) {
            dataMapper = new SqlDataMapper<TicketWare>(SqlDbType.Postgresql, conn, new TicketWareBuilder());
        }


        public long Create(TicketWare ticketWare) {
            const string sql =
                "INSERT INTO public.\"TicketWare\"(\"Id\", \"TicketId\", \"WareId\") VALUES (DEFAULT, @ticketId, @wareId) returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("ticketId", ticketWare.TicketId),
                new NpgsqlParameter("wareId", ticketWare.WareId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"TicketWare\" WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(TicketWare ticketWare) {
            const string sql =
                "UPDATE public.\"TicketWare\" SET \"TicketId\"= @ticketId, \"WareId\"= @wareId WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ticketWare.Id),
                new NpgsqlParameter("ticketId", ticketWare.TicketId),
                new NpgsqlParameter("wareId", ticketWare.WareId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public List<TicketWare> GetAll() {
            const string sql = "SELECT * FROM \"TicketWare\"" + innerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<TicketWare> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"TicketWare\"" + innerJoin +
                               " ORDER BY \"TicketWare\".\"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public TicketWare GetById(long id) {
            const string sql = "SELECT * FROM \"TicketWare\"" + innerJoin + "WHERE \"TicketWare\".\"Id\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public TicketWare GetByTicketId(long id) {
            const string sql = "SELECT * FROM \"TicketWare\"" + innerJoin + "WHERE \"TicketId\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }

    }

}