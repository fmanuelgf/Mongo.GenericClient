namespace Mongo.GenericClient.Services
{
    using System.Threading.Tasks;
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Services;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class WriteService<TEntity> : IWriteService<TEntity>
        where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> collection;

        public WriteService(IMongoContext context)
        {
            this.collection = context.GetCollection<TEntity>();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            await this.collection
                .InsertOneAsync(entity);

            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, entity.Id);

            var result = await this.collection
                .ReplaceOneAsync(filter, entity);

            return result.ModifiedCount == 1;
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<TEntity>
                .Filter
                .Eq(x => x.Id, id);

            var result = await this.collection
                .DeleteOneAsync(filter);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException($"'{id}' is not a valid ObjectId");
            }
            
            await this.DeleteAsync(objectId);
        }
    }
}