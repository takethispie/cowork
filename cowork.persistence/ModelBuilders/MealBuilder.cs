using System;
using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class MealBuilder : IModelBuilderSql<Meal> {

        public Meal CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var meal = new Meal {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                Date = dbHandler.GetValue<DateTime>(1 + startingIndex),
                Description = dbHandler.GetValue<string>(2 + startingIndex),
                PlaceId = dbHandler.GetValue<long>(3 + startingIndex)
            };
            nextStartingIndex = 4 + startingIndex;
            return meal;
        }

    }

}