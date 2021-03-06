using System.Collections.Generic;
using System.Data.Common;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using cowork.persistence.Handlers;
using cowork.persistence.ModelBuilders;
using Npgsql;

namespace cowork.persistence.Repositories {

    public class TicketCommentRepository : ITicketCommentRepository {

        private const string InnerJoin = " INNER JOIN \"Users\" U on \"TicketComment\".\"AuthorId\" = U.\"Id\" ";

        private readonly SqlDataMapper<TicketComment> dataMapper;


        public TicketCommentRepository(string conn) {
            dataMapper = new SqlDataMapper<TicketComment>(SqlDbType.Postgresql, conn, new TicketCommentBuilder());
        }


        public long Create(TicketComment ticketComment) {
            const string sql =
                "INSERT INTO public.\"TicketComment\"(\"Id\", \"Content\", \"TicketId\", \"AuthorId\", \"Created\") VALUES (DEFAULT, @content, @ticketId, @authorId, @created) returning \"Id\";";
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
                "UPDATE public.\"TicketComment\" SET \"Content\"= @content, \"TicketId\"= @ticketId, \"AuthorId\"= @authorId, \"Created\"= @created WHERE \"Id\"= @id returning \"Id\";";
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
            const string sql = "SELECT * FROM \"TicketComment\"" + InnerJoin + ";";
            return dataMapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public List<TicketComment> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"TicketComment\"" + InnerJoin +
                               " ORDER BY \"TicketComment\".\"Created\" ASC LIMIT @amount OFFSET @skip;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", page * amount)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public TicketComment GetById(long id) {
            const string sql = "SELECT * FROM \"TicketComment\"" + InnerJoin + "WHERE \"TicketComment\".\"Id\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.OneItemCommand(sql, par);
        }


        public List<TicketComment> GetByTicketId(long ticketId) {
            const string sql = "SELECT * FROM \"TicketComment\"" + InnerJoin + "WHERE \"TicketId\"= @id";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", ticketId)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }


        public List<TicketComment> LastCommentsFromUser(long userId, int numberOfComments) {
            const string sql = "SELECT * FROM \"TicketComment\"" + InnerJoin + "WHERE \"AuthorId\"= @userID ORDER BY \"Created\" DESC LIMIT @numComments;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("userId", userId),
                new NpgsqlParameter("numComments", numberOfComments)
            };
            return dataMapper.MultiItemCommand(sql, par);
        }

    }

}