using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class UserRepository : IUserRepository {

        private readonly SqlDataMapper<User> datamapper;


        public UserRepository(string connectionString) {
            datamapper = new SqlDataMapper<User>(SqlDbType.Postgresql, connectionString, new UserBuilder());
        }


        public List<User> GetAll() {
            const string sql = "SELECT * FROM public.\"Users\";";
            return datamapper.MultiItemCommand(sql, null);
        }


        public User GetById(long id) {
            const string sql = "SELECT * FROM public.\"Users\" WHERE \"Id\" = @p";
            var parameters = new List<DbParameter> {new NpgsqlParameter("p", id)};
            return datamapper.OneItemCommand(sql, parameters);
        }


        public List<User> GetAllWithPaging(int page, int amount) {
            const string sql = "SELECT * FROM \"Users\" ORDER BY \"Id\" ASC LIMIT @amount OFFSET @skip";
            var par = new List<DbParameter> {
                new NpgsqlParameter("amount", amount),
                new NpgsqlParameter("skip", amount * page)
            };
            return datamapper.MultiItemCommand(sql, par);
        }


        public long Update(User user) {
            const string sql =
                "UPDATE public.\"Users\" SET \"FirstName\" = @firstName, \"LastName\" = @lastName, \"IsStudent\" = @isStudent, \"Type\"= @type WHERE \"Id\" = @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", user.Id),
                new NpgsqlParameter("firstName", user.FirstName),
                new NpgsqlParameter("lastName", user.LastName),
                new NpgsqlParameter("isStudent", user.IsAStudent),
                new NpgsqlParameter("type", (long) user.Type)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }


        public bool DeleteById(long userId) {
            const string sql = "DELETE FROM public.\"Users\" WHERE \"Id\" = @p RETURNING \"Id\";";
            var parameters = new List<DbParameter> {new NpgsqlParameter("p", userId)};
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(User user) {
            const string sql =
                "INSERT into public.\"Users\" (\"Id\", \"FirstName\", \"LastName\", \"IsStudent\", \"Type\") VALUES (DEFAULT, @firstName, @lastName, @isStudent, @type) RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("firstName", user.FirstName),
                new NpgsqlParameter("lastName", user.LastName),
                new NpgsqlParameter("isStudent", user.IsAStudent),
                new NpgsqlParameter("type", (long) user.Type)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }

    }

}