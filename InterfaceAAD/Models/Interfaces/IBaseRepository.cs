using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAAD.Models.Interfaces
{
    /// <summary>
    /// Interface defining the basic repository operations for a specific model.
    /// </summary>
    /// <typeparam name="M">The type of the model for which the repository is defined.</typeparam>
    internal interface IBaseRepository<M>
    {
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the entity is added successfully, otherwise false.</returns>
        Task<bool> Add(M entity);

        /// <summary>
        /// Edits an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be edited.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the entity is edited successfully, otherwise false.</returns>
        Task<bool> Edit(M entity);

        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task representing the asynchronous operation. Returns the entity with the specified identifier, or null if not found.</returns>
        Task<M> GetById(int id);

        /// <summary>
        /// Gets a list of all entities in the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns a list of entities.</returns>
        Task<List<M>> GetByAll();
    }
}
