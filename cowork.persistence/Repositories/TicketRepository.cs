using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class TicketRepository : ITicketRepository {

        private const string InnerJoin =
            " inner join \"Users\" U on \"Tickets\".\"OpenedBy\" = U.\"Id\" inner join \"Place\" P on \"Tickets\".\"PlaceId\" = P.\"Id\" ";

        private readonly SqlDataMapper<Ticket> dataMapper;
        private SqlDataMapper<TicketWare> ticketWareDataMapper;


        public TicketRepository(string connection) {
            dataMapper = new SqlDataMapper<Ticket>(SqlDbType.Postgresql, connection, new TicketBuilder());
        }


        public List<Ticket> GetAll() {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<Ticket> GetAllOfPlace(long placeId) {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin + "WHERE \"Tickets\".\"PlaceId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", placeId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public Ticket GetById(long id) {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin + "WHERE \"Tickets\".\"Id\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<Ticket> GetAllOpenedBy(long userId) {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin + "WHERE \"Tickets\".\"OpenedBy\"= @id;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", userId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Ticket> GetAllByPaging(int page, int amount) {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin +
                               " ORDER BY \"Tickets\".\"Created\" DESC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<Ticket> GetAllWithState(int state) {
            const string sql = "SELECT * FROM public.\"Tickets\"" + InnerJoin + "WHERE \"Tickets\".\"State\"= @state;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("state", state)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM public.\"Tickets\" WHERE \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(Ticket ticket) {
            var sql =
                "UPDATE public.\"Tickets\" SET \"OpenedBy\"= @openedby, \"State\"= @state, \"Description\"= @description, \"PlaceId\"= @placeId, \"Title\"= @title, \"Created\"= @created WHERE \"Tickets\".\"Id\"= @id RETURNING \"Tickets\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ticket.Id),
                new NpgsqlParameter("openedBy", ticket.OpenedById),
                new NpgsqlParameter("state", (short) ticket.State),
                new NpgsqlParameter("description", ticket.Description),
                new NpgsqlParameter("placeId", ticket.PlaceId),
                new NpgsqlParameter("title", ticket.Title),
                new NpgsqlParameter("created", ticket.Created)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public long Create(Ticket ticket) {
            var sql =
                "INSERT INTO public.\"Tickets\"(\"Id\", \"OpenedBy\", \"State\", \"Description\", \"PlaceId\", \"Title\", \"Created\")VALUES (DEFAULT, @openedBy, @state, @description, @placeId, @title, @created) RETURNING \"Tickets\".\"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("openedBy", ticket.OpenedById),
                new NpgsqlParameter("state", (short) ticket.State),
                new NpgsqlParameter("description", ticket.Description),
                new NpgsqlParameter("placeId", ticket.PlaceId),
                new NpgsqlParameter("title", ticket.Title),
                new NpgsqlParameter("created", ticket.Created)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }

    }

}