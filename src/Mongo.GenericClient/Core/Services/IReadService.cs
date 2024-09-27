namespace Mongo.GenericClient.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Models;
    using MongoDB.Bson;
    using MongoDB.Driver.Linq;

    public interface IReadService<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Get all the documents.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{TEntity}"/> mapping the documents.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Creates a queryable source of documents.
        /// </summary>
        /// <returns>The <see cref="IMongoQueryable{TEntity}"/>.</returns>
        IMongoQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Get a paginated result.
        /// </summary>
        /// <param name="pageNum">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The <see cref="PaginationResult{TEntity}"/>.</returns>
        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);

        /// <summary>
        /// Get a document.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        /// <returns>The <see cref="TEntity"/> mapping the document.</returns>
        Task<TEntity> GetByIdAsync(ObjectId id);
    }
}