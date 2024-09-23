namespace Mongo.Generics.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Models;
    using MongoDB.Bson;

    public interface IReadService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        IEnumerable<TEntity> GetAll(bool includeDeleted = false);

        Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize);

        Task<TEntity> GetByIdAsync(ObjectId id);
    }
}