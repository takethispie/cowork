using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class TicketCommentRepository : ITicketCommentRepository {

        private SqlDataMapper<TicketComment> dataMapper;
        private const string innerJoin = " INNER JOIN \"Users\" U on \"TicketComment\".\"AuthorId\" = U.\"Id\" ";


        public TicketCommentRepository(string conn) {
            dataMapper = new SqlDataMapper<TicketComment>(SqlDbType.Postgresql, conn, new TicketCommentBuilder());
        }


        public long Create(TicketComment ticketComment) {
            const string sql = "INSERT INTO public.\"TicketComment\"(\"Id\", \"Content\", \"TicketId\", \"AuthorId\", \"Created\") VALUES (DEFAULT, @content, @ticketId, @authorId, @created) returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("content", ticketComment.Content),
                new NpgsqlParameter("ticketId", ticketComment.TicketId),
                new NpgsqlParameter("authorId", ticketComment.AuthorId),
                new NpgsqlParameter("created", ticketComment.Created)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"TicketComment\" WHERE \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Update(TicketComment ticketComment) {
            const string sql =
                "UPDATE public.\"TicketComment\" SET \"Id\"= @id, \"Content\"= @content, \"TicketId\"= @ticketId, \"AuthorId\"= @authorId, \"Created\"= @created WHERE \"Id\"= @id returning \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ticketComment.Id),
                new NpgsqlParameter("content", ticketComment.Content),
                new NpgsqlParameter("ticketId", ticketComment.TicketId),
                new NpgsqlParameter("authorId", ticketComment.AuthorId),
                new NpgsqlParameter("created", ticketComment.Created)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public List<TicketComment> GetAll() {
            const string sql = "SELECT * FROM \"TicketComment\"" + innerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public TicketComment GetById(long id) {
            const string sql = "SELECT * FROM \"TicketComment\"" + innerJoin + "WHERE \"TicketComment\".\"Id\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<TicketComment> GetByTicketId(long ticketId) {
            const string sql = "SELECT * FROM \"TicketComment\"" + innerJoin + "WHERE \"TicketId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ticketId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }

    }

}