using System;
using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class UserBuilder : IModelBuilderSql<User> {

        public User CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var successParsing =
                Enum.TryParse<UserType>(dbHandler.GetValue<short>(4 + startingIndex).ToString(), out var type);
            if (!successParsing) throw new Exception("Error parsing RoomType");
            var user = new User();
            user.Id = dbHandler.GetValue<long>(0 + startingIndex);
            user.FirstName = dbHandler.GetValue<string>(1 + startingIndex);
            user.LastName = dbHandler.GetValue<string>(2 + startingIndex);
            user.IsAStudent = dbHandler.GetValue<bool>(3 + startingIndex);
            user.Type = type;
            nextStartingIndex = 5 + startingIndex;
            return user;
        }
    }

}