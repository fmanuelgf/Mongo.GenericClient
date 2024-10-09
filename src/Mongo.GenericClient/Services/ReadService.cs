namespace Mongo.GenericClient.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public class ReadService<TEntity> : IReadService<TEntity>
        where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> collection;

        public ReadService(IMongoContext context)
        {
            this.collection = context.GetCollection<TEntity>();
        }

        /// <inheritdoc />
        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return this.collection
                .Find(filter ?? Builders<TEntity>.Filter.Empty)
                .ToList();
        }

        /// <inheritdoc />
        public virtual IMongoQueryable<TEntity> AsQueryable()
        {
            return this.collection
                .AsQueryable();
        }

        /// <inheritdoc />
        public virtual Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize)
        {
            return this.GetPaginatedAsync(pageNum, pageSize, null);
        }

        /// <inheritdoc />
        public virtual Task<PaginationResult<TEntity>> GetPaginatedAsync(
            Expression<Func<TEntity, bool>>? filter,
            int pageNum,
            int pageSize)
        {
            return this.GetPaginatedAsync(pageNum, pageSize, filter);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, id);

            return await this.collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                return await this.GetByIdAsync(objectId);
            }
            
            throw new ArgumentException($"'{id}' is not a valid ObjectId");
        }

        private async Task<PaginationResult<TEntity>> GetPaginatedAsync(
            int pageNum,
            int pageSize,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            var query = filter ?? Builders<TEntity>.Filter.Empty;
            var totalCount = this.collection
                .CountDocuments(query);

            var entities = await this.collection
                .Find(query)
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
    }
}