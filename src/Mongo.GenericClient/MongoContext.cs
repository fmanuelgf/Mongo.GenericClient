namespace Mongo.GenericClient
{
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public class MongoContext : IMongoContext
    {
        public MongoContext(MongoClientSettings? settings = default)
        {
            MongoHelper.Client = new MongoClient(settings ?? AppConfig.DefaultMongoClientSettings);
            MongoHelper.Database = MongoHelper.Client.GetDatabase(AppConfig.DatabaseName);
        }
        
        /// <inheritdoc />
        public IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity
        {
            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            return MongoHelper.Database.GetCollection<TEntity>(collectionName?.Name);
        }
    }
}