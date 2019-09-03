using System;

namespace coworkpersistence.Handlers {

    /// <summary>
    ///     Factory utilisé pour crée des ISqlHandler
    ///     en fonction de la bdd passé en parametre
    /// </summary>
    internal class SqlDbHandlerFactory {

        public ISqlDbHandler CreateHandler(SqlDbType dbType, string connectionString) {
            switch (dbType) {
                case SqlDbType.Postgresql:
                    return new PostgresHandler(connectionString);

                default:
                    throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null);
            }
        }

    }

}