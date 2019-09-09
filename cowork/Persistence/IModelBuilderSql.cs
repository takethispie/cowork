using coworkpersistence.Handlers;

namespace coworkpersistence {

    /// <summary>
    ///     interface d'une class capable de creer un objet à partir des données en base
    /// </summary>
    /// <typeparam name="T">type de l'objet retourné</typeparam>
    public interface IModelBuilderSql<out T> {

        T CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex);

    }

}