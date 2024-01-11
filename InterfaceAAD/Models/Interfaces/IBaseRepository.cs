namespace InterfaceAAD.Models.Interfaces
{
    /// <summary>
    /// Interface defining basic repository operations for a specific model.
    /// </summary>
    /// <typeparam name="M">The type of the model for which the repository is defined.</typeparam>
    internal interface IBaseRepository<M>
    {
        //bool Save(M entity);
        
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A boolean indicating whether the entity is added successfully or not.</returns>
        bool Add(M entity);

        /// <summary>
        /// Edits an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be edited.</param>
        /// <returns>A boolean indicating whether the entity is edited successfully or not.</returns>
        bool Edit(M entity);

        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        M GetById(int id);

        /// <summary>
        /// Gets a list of all entities in the repository.
        /// </summary>
        /// <returns>A list of entities.</returns>
        List<M> GetAll();
    }
}
