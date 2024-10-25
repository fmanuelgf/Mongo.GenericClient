namespace Mongo.GenericClient.Core.Services
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Models;
    using MongoDB.Bson;
    using MongoDB.Driver.Linq;

    public interface IReadService<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Get all the documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="filter">The filter to be applied (if not null).</param>
        /// <returns>The <see cref="IList{TEntity}"/> mapping the documents.</returns>
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Get a paginated result containing a list of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="pageNum">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The <see cref="PaginationResult{TEntity}"/>.</returns>
        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);
        
        /// <summary>
        /// Get a paginated result containing a list of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="filter">The filter to be applied.</param>
        /// <param name="pageNum">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The <see cref="PaginationResult{TEntity}"/>.</returns>
        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            Expression<Func<TEntity, bool>> filter,
            int pageNum,
            int pageSize);

        /// <summary>
        /// Get a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        /// <returns>The <see cref="TEntity"/> mapping the document.</returns>
        Task<TEntity> GetByIdAsync(ObjectId id);

        /// <summary>
        /// Get a document of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        /// <returns>The <see cref="TEntity"/> mapping the document.</returns>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Creates a queryable source of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <returns>The <see cref="IMongoQueryable{TEntity}"/>.</returns>
        IMongoQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Get the count of documents of type <see cref="TEntity"/>.
        /// </summary>
        /// <param name="filter">The filter to be applied.</param>
        /// <returns>The count as <see cref="long"></returns>
        long CountDocuments(Expression<Func<TEntity, bool>>? filter = null);
    }
}