namespace Mongo.Generics.Tests.Base
{
    using System;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Core.Repositories;
    using Mongo.Generics.Core.Services;
    using Mongo.Generics.Repositories;
    using Mongo.Generics.Services;
    using Mongo.Generics.Tests.SetUp;
    using MongoDB.Driver;

    public abstract class ScenarioBase<TEntity> : IDisposable
        where TEntity : AuditableEntity, IEntity
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