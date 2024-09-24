namespace Mongo.Generics.Services
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
        where TEntity : IEntity
    {
        private readonly IGenericRepository<TEntity> repository;

        public ReadService(
            IGenericRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.repository
                .Collection
                .AsQueryable()
                .ToList();
        }

        public virtual async Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            var filter = Builders<TEntity>.Filter.Empty;
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

        public virtual async Task<TEntity> GetByIdAsync(ObjectId id)
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