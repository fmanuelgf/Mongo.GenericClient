namespace Mongo.GenericClient
{
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase database;

        public MongoContext(MongoClientSettings? settings = default)
        {
            var client = new MongoClient(settings ?? AppConfig.DefaultMongoClientSettings);
            this.database = client.GetDatabase(AppConfig.DatabaseName);
        }
        
        /// <inheritdoc />
        public IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity
        {
            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            return this.database.GetCollection<TEntity>(collectionName?.Name);
        }
    }
}