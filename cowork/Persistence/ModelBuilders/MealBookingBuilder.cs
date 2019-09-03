using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class MealBookingBuilder : IModelBuilderSql<MealBooking> {

        public MealBooking CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var userBuilder = new UserBuilder();
            var mealBuilder = new MealBuilder();
            var mealRes = new MealBooking();
            mealRes.Id = dbHandler.GetValue<long>(0 + startingIndex);
            mealRes.MealId = dbHandler.GetValue<long>(1 + startingIndex);
            mealRes.UserId = dbHandler.GetValue<long>(2 + startingIndex);
            mealRes.Note = dbHandler.GetValue<string>(3 + startingIndex);
            
            mealRes.Meal = mealBuilder.CreateDomainModel(dbHandler, 4 + startingIndex, out nextStartingIndex);
            mealRes.User = userBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            return mealRes;
        }

    }

}