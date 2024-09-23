namespace Mongo.Generics.Services
{
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Core.Services;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class WriteService<TEntity> : IWriteService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        private readonly IGenericRepository<TEntity> repository;

        public WriteService(
            IGenericRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            entity.CreatedAt = DateTime.Now;

            await this.repository
                .Collection
                .InsertOneAsync(entity);

            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, entity.Id);

            entity.UpdatedAt = DateTime.Now;

            var result = await this.repository
                .Collection
                .ReplaceOneAsync(filter, entity);

            return result.ModifiedCount == 1;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, entity.Id);

            entity.DeletedAt = DateTime.Now;

            var result = await this.repository
                .Collection
                .ReplaceOneAsync(filter, entity);

            return result.ModifiedCount == 1;
        }
    }
}