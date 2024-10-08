namespace Mongo.GenericClient
{
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase database;

        public MongoContext()
        {
            var client = new MongoClient(AppConfig.ConnectionString);
            this.database = client.GetDatabase(AppConfig.DatabaseName);
        }
        
        /// <summary>
        /// Get the collection of type <see cref="TEntity"/>.
        /// </summary>
        /// <returns>The <see cref="IMongoCollection"/>.</returns>
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