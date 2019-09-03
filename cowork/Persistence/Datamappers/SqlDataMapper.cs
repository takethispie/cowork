using System;
using System.Collections.Generic;
using System.Data.Common;
using coworkpersistence.Handlers;

namespace coworkpersistence.Datamappers {

    /// <summary>
    ///     datamapper pour une BDD SQL
    /// </summary>
    /// <typeparam name="T">type de l'objet métier manipulé par ce datamapper</typeparam>
    public class SqlDataMapper<T> where T : class {

        //object accedant directement à la bdd
        private readonly ISqlDbHandler dbHandler;

        //builder du domain object
        private readonly IModelBuilderSql<T> builder;


        public SqlDataMapper(SqlDbType type, string connectionString, IModelBuilderSql<T> builder) {
            this.builder = builder ?? throw new Exception("instance du paramètre builder null");
            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection String is empty");
            dbHandler = new SqlDbHandlerFactory().CreateHandler(type, connectionString);
        }


        /// <summary>
        ///     execute une requete sql retournant un seul objet métier
        /// </summary>
        /// <param name="sql">requete sql à executer</param>
        /// <param name="parameters">parametres de la requete à injecter</param>
        /// <returns>l'objet métier</returns>
        public T OneItemCommand(string sql, List<DbParameter> parameters) {
            if(parameters == null) parameters = new List<DbParameter>();
            dbHandler.ExecuteCommand(sql, parameters);
            if (!dbHandler.Read()) {
                dbHandler.EndCommand();
                return null;
            }

            var result = builder.CreateDomainModel(dbHandler, 0, out var nextIndex);
            dbHandler.EndCommand();
            return result;
        }


        /// <summary>
        /// execute une requete sql count 
        /// </summary>
        /// <param name="sql">requete sql COUNT</param>
        /// <param name="parameters">paramètres à injecter dans la requête SQL</param>
        /// <returns></returns>
        public long CountCommand(string sql, List<DbParameter> parameters) {
            if(parameters == null) parameters = new List<DbParameter>();
            dbHandler.ExecuteCommand(sql, parameters);
            if (!dbHandler.Read()) {
                dbHandler.EndCommand();
                return -1;
            }

            var result = dbHandler.GetValue<long>(0);
            dbHandler.EndCommand();
            return result;
        }


        /// <summary>
        ///     retourne une liste d'objet metier du type T
        /// </summary>
        /// <param name="sql">la requete sql à executer</param>
        /// <param name="parameters">parametres de la requete à injecter</param>
        /// <returns>une liste de contact</returns>
        public List<T> MultiItemCommand(string sql, List<DbParameter> parameters) {
            var objects = new List<T>();
            if(parameters == null) parameters = new List<DbParameter>();
            dbHandler.ExecuteCommand(sql, parameters);
            while (dbHandler.Read()) {
                var dObj = builder.CreateDomainModel(dbHandler, 0, out var nextIndex);
                if (dObj != null) objects.Add(dObj);
            }
            dbHandler.EndCommand();
            return objects;
        }


        /// <summary>
        ///     execute une commande qui ne retourne pas de donnée
        /// </summary>
        /// <param name="sql">requete sql</param>
        /// <param name="parameters">parametre de la requete à injecter</param>
        /// <returns>nombre de row impactés</returns>
        public long NoQueryCommand(string sql, List<DbParameter> parameters) {
            if(parameters == null) parameters = new List<DbParameter>();
            var res = dbHandler.ExecuteNonQueryCommand(sql, parameters);
            dbHandler.EndCommand();
            if (!res.HasValue) return -1;
            return res.Value;
        }

    }

}