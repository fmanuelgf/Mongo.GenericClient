namespace Mongo.GenericClient
{
    using MongoDB.Driver;

    /// <summary>
    /// Static class providing access to MongoClient and IMongoDatabase.
    /// </summary>
    public static class MongoHelper
    {
        /// <summary>
        /// The <see cref="MongoClient"/>, autoconnected to the configured database (see <see cref="AppConfig"/>)
        /// </summary>
        public static MongoClient Client { get; internal set; } = new MongoClient(AppConfig.ConnectionString);
        
        /// <summary>
        /// The <see cref="IMongoDatabase"/> (see <see cref="AppConfig"/>)
        /// </summary>
        public static IMongoDatabase Database { get; internal set; } = Client.GetDatabase(AppConfig.DatabaseName);
    }
}