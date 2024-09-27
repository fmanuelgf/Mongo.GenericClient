namespace Mongo.GenericClient.Tests.Base
{
    using System;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Repositories;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Repositories;
    using Mongo.GenericClient.Services;
    using Mongo.GenericClient.Tests.SetUp;
    using MongoDB.Driver;

    public abstract class ScenarioBase<TEntity> : IDisposable
        where TEntity : IEntity
    {
        protected IGenericRepository<TEntity> Repository { get; set; }
        
        protected IReadService<TEntity> ReadService { get; set; }
        
        protected IWriteService<TEntity> WriteService { get; set; }

        public ScenarioBase()
        {
            TestConfig.Configure();

            this.Repository = new GenericRepository<TEntity>();
            this.ReadService = new ReadService<TEntity>(this.Repository);
            this.WriteService = new WriteService<TEntity>(this.Repository);
        }

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