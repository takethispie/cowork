using System.Collections.Generic;
using System.Data.Common;
using coworkdomain;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class LoginRepository : ILoginRepository {

        private readonly SqlDataMapper<Login> dataMapper;


        public LoginRepository(string conn) {
            dataMapper = new SqlDataMapper<Login>(SqlDbType.Postgresql, conn, new LoginBuilder());
        }


        public long Create(Login login) {
            const string sql =
                "INSERT INTO public.\"Login\"(\"Id\", \"PasswordHash\", \"PasswordSalt\", \"UserId\", \"Email\") VALUES (DEFAULT, @passwordHash, @passwordSalt, @userId, @email) RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", login.Id),
                new NpgsqlParameter("passwordHash", login.PasswordHash),
                new NpgsqlParameter("passwordSalt", login.PasswordSalt),
                new NpgsqlParameter("userId", login.UserId),
                new NpgsqlParameter("email", login.Email)
            };
            return dataMapper.NoQueryCommand(sql, par);
        }


        public bool Delete(long id) {
            const string sql = "DELETE FROM \"Login\" where \"Id\"= @id RETURNING \"Id\";";
            var par = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return dataMapper.NoQueryCommand(sql, par) > -1;
        }


        public long Auth(string email, string password) {
            const string sql = "SELECT * FROM \"Login\" where \"Email\"= @email;";
            var par = new List<DbParameter> {
                new NpgsqlParameter("email", email)
            };
            var login = dataMapper.OneItemCommand(sql, par);
            if (login == null) return -1;
            var result = PasswordHashing.VerifyPasswordHash(password, login.PasswordHash, login.PasswordSalt);
            return result ? login.UserId : -1;
        }

    }

}