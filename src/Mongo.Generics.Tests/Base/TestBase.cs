namespace Mongo.Generics.Tests.Base
{
    using System;
    using MongoDB.Driver;

    public abstract class TestBase : IDisposable
    {
        public void Dispose()
        {
            var client = new MongoClient(AppConfig.ConnectionString);
            client.DropDatabase(AppConfig.DatabaseName);
        }
    }
}