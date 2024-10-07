namespace Mongo.GenericClient
{
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    /// <summary>
    /// Static class providing access to MongoClient and IMongoDatabase.
    /// </summary>
    public static class MongoHelper
    {
        private static readonly MongoClient client = new MongoClient(AppConfig.ConnectionString);
        private static readonly IMongoDatabase database = client.GetDatabase(AppConfig.DatabaseName);

        /// <summary>
        /// The <see cref="MongoClient"/>, autoconnected to the configured database (see <see cref="AppConfig"/>)
        /// </summary>
        public static MongoClient Client => client;
        
        /// <summary>
        /// The <see cref="IMongoDatabase"/> (see <see cref="AppConfig"/>)
        /// </summary>
        public static IMongoDatabase Database => database;

        /// <summary>
        /// Get the collection of type <see cref="TEntity"/>.
        /// </summary>
        /// <returns>The <see cref="IMongoCollection"/>.</returns>
        public static IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity
        {
            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            return Database.GetCollection<TEntity>(collectionName?.Name);
        }
    }
}