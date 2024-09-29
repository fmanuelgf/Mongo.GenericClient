namespace Mongo.GenericClient
{
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
    }
}