namespace Mongo.Generics.Tests.Base
{
    using System;
    using MongoDB.Driver;

    public abstract class ScenarioBase : IDisposable
    {
        public void ClearDatabase()
        {
            var client = new MongoClient(AppConfig.ConnectionString);
            client.DropDatabase(AppConfig.DatabaseName);
        }

        public void Dispose()
        {
            this.ClearDatabase();
        }
    }
}