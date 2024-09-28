namespace Mongo.GenericClient.Tests.Base
{
    using System;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Repositories;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Tests.IoC;
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
            Dependencies.Configure();
            
            this.Repository = Dependencies.GetRequiredService<IGenericRepository<TEntity>>();
            this.ReadService = Dependencies.GetRequiredService<IReadService<TEntity>>();
            this.WriteService = Dependencies.GetRequiredService<IWriteService<TEntity>>();
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