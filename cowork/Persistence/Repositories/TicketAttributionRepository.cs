using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class TicketAttributionRepository : ITicketAttributionRepository {

        private readonly SqlDataMapper<TicketAttribution> dataMapper;


        public TicketAttributionRepository(string conn) {
            dataMapper =
                new SqlDataMapper<TicketAttribution>(SqlDbType.Postgresql, conn, new TicketAttributionBuilder());
        }


        public List<TicketAttribution> GetAll() {
            const string sql = "SELECT * FROM \"TicketAttribution\";";
            var par = new List<DbParameter>();
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<TicketAttribution> GetAllWithPaging(int page, int amount) {
            const string sql =
                "SELECT * FROM \"TicketAttribution\" ORDER BY \"TicketAttribution\".\"Id\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public TicketAttribution GetById(long id) {
            const string sql = "SELECT * FROM \"TicketAttribution\" WHERE \"Id\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<TicketAttribution> GetAllFromStaffId(long id) {
            const string sql = "SELECT * FROM \"TicketAttribution\" WHERE \"StaffId\"= @staffId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("staffId", id)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public TicketAttribution GetFromTicket(long ticketId) {
            const string sql = "SELECT * FROM \"TicketAttribution\" WHERE \"TicketId\"= @ticketId;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("ticketid", ticketId)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"TicketAttribution\" WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Create(TicketAttribution ticketAttribution) {
            const string sql =
                "INSERT INTO public.\"TicketAttribution\"(\"Id\", \"StaffId\", \"TicketId\") VALUES (DEFAULT, @staffId, @ticketId) RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("staffId", ticketAttribution.StaffId),
                new NpgsqlParameter("ticketId", ticketAttribution.TicketId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public long Update(TicketAttribution ticketAttribution) {
            const string sql =
                "UPDATE public.\"TicketAttribution\" SET \"Id\"= @id, \"StaffId\"= @staffId, \"TicketId\"= @ticketId WHERE \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("staffId", ticketAttribution.StaffId),
                new NpgsqlParameter("ticketId", ticketAttribution.TicketId)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }

    }

}