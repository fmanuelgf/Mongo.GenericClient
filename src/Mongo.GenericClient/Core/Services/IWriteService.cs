namespace Mongo.GenericClient.Core.Services
{
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Bson;

    public interface IWriteService<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Create a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/>.</param>
        /// <returns>The created <see cref="TEntity"/> mapping the document.</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Update a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> mapping the document.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The <see cref="TEntity"/> ID.</param>
        /// <param name="data">The field-value pairs to update.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> UpdateAsync(ObjectId id, Dictionary<string, object> data);

        /// <summary>
        /// Update a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The <see cref="TEntity"/> ID.</param>
        /// <param name="data">The field-value pairs to update.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> UpdateAsync(string id, Dictionary<string, object> data);

        /// <summary>
        /// Delete a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        Task DeleteAsync(ObjectId id);

        /// <summary>
        /// Delete an array of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="ids">The array of IDs of the documents to delete.</param>
        Task DeleteAsync(ObjectId[] ids);

        /// <summary>
        /// Delete an array of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="ids">The array of IDs of the documents to delete.</param>
        Task DeleteAsync(IList<ObjectId> ids);

        /// <summary>
        /// Delete a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The ID of the document to delete.</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// Delete an array of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="ids">The array of IDs of the documents to delete.</param>
        Task DeleteAsync(string[] ids);

        /// <summary>
        /// Delete an array of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="ids">The array of IDs of the documents to delete.</param>
        Task DeleteAsync(IList<string> ids);
    }
}