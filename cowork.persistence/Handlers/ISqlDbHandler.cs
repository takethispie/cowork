using System.Collections.Generic;
using System.Data.Common;

namespace cowork.persistence.Handlers {

    /// <summary>
    ///     permet d'executer des commandes
    ///     et recuperer les valeurs
    /// </summary>
    public interface ISqlDbHandler {

        /// <summary>
        ///     execute une commande sql de type SELECT
        /// </summary>
        /// <param name="sql">commande sql à executer</param>
        void ExecuteCommand(string sql, List<DbParameter> parameters);


        /// <summary>
        ///     execute une command sql qui n'est pas de type SELECT
        ///     tel que UPDATE, INSERT, etc
        /// </summary>
        /// <param name="sql">commande sql à executer</param>
        /// <returns></returns>
        long? ExecuteNonQueryCommand(string sql, List<DbParameter> parameters);


        /// <summary>
        ///     extrait la valeur d'une colonne d'un row de la source de donnée
        /// </summary>
        /// <param name="columnId">id de la colomne à lire</param>
        /// <typeparam name="T">type dans lequel cast la valeur</typeparam>
        /// <returns>valeur de type T</returns>
        T GetValue<T>(int columnId);


        /// <summary>
        ///     charge le prochain row depuis la source de donnée
        /// </summary>
        /// <returns>true si un row à été chargé, false s'il n'y a plus de row à charger</returns>
        bool Read();


        /// <summary>
        ///     ferme la commande
        /// </summary>
        void EndCommand();

    }

}