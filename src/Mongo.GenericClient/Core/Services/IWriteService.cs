namespace Mongo.GenericClient.Core.Services
{
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Bson;

    public interface IWriteService<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Create a document.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/>.</param>
        /// <returns>The created <see cref="TEntity"/> mapping the document.</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Update a document.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> mapping the document.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        Task DeleteAsync(ObjectId id);

        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        Task DeleteAsync(string id);
    }
}