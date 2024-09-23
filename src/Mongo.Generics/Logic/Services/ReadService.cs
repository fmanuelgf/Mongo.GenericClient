namespace Mongo.Generics.Logic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Core.Services;
    using Mongo.Generics.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class ReadService<TEntity> : IReadService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        private readonly IGenericRepository<TEntity> repository;

        public ReadService(
            IGenericRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<TEntity> GetAll(bool includeDeleted = false)
        {
            return this.repository
                .Collection
                .AsQueryable()
                .Where(x => includeDeleted || x.DeletedAt == null)
                .ToList();
        }

        public async Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }

            if (pageSize < 5)
            {
                pageSize = 5;
            }

            var filter = Builders<TEntity>.Filter.Eq(x => x.DeletedAt, null);
            var totalCount = this.repository
                .Collection
                .CountDocuments(filter);

            var entities = await this.repository
                .Collection
                .Find(filter)
                .Skip((pageNum - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PaginationResult<TEntity>()
            {
                Page = pageNum,
                PageSize = pageSize,
                TotalItems = totalCount,
                Result = entities
            };
        }

        public async Task<TEntity> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, id);

            return await this.repository
                .Collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }
    }
}