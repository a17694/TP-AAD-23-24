namespace InterfaceAAD.Models.Interfaces
{
    /// <summary>
    /// Defines the basic repository operations for a specific model.
    /// </summary>
    /// <typeparam name="M">The type of the model for which the repository is defined.</typeparam>
    internal interface IBaseRepository<M>
    {
        /// <summary>
        /// Saves an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>True if the save operation is successful, otherwise false.</returns>
        bool Save(M entity);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        M GetById(int id);

        /// <summary>
        /// Retrieves a list of all entities in the repository.
        /// </summary>
        /// <returns>A list of entities.</returns>
        List<M> GetAll();
    }
}