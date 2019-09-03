using coworkdomain;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class LoginBuilder : IModelBuilderSql<Login> {

        public Login CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var login = new Login {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                PasswordHash = dbHandler.GetValue<byte[]>(1 + startingIndex),
                PasswordSalt = dbHandler.GetValue<byte[]>(2 + startingIndex),
                UserId =  dbHandler.GetValue<long>(3 + startingIndex),
                Email =  dbHandler.GetValue<string>(4 + startingIndex)
            };
            nextStartingIndex = 5 + startingIndex;
            return login;
        }

    }

}