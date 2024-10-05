namespace Mongo.GenericClient.Tests.Base
{
    using System;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Repositories;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Tests.IoC;
    using Mongo.GenericClient.Tests.SetUp;

    public abstract class ScenarioBase<TEntity> : IDisposable
        where TEntity : IEntity
    {
        public IGenericRepository<TEntity> Repository { get; set; }
        
        public IReadService<TEntity> ReadService { get; set; }
        
        public IWriteService<TEntity> WriteService { get; set; }

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
            MongoHelper.Client.DropDatabase(AppConfig.DatabaseName);
        }

        public void Dispose()
        {
            this.ClearDatabase();
        }
    }
}