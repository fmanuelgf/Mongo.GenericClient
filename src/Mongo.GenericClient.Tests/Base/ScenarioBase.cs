namespace Mongo.GenericClient.Tests.Base
{
    using System;
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Tests.Setup;
    using MongoDB.Driver;

    public abstract class ScenarioBase<TEntity> : IDisposable
        where TEntity : IEntity
    {
        protected IMongoCollection<TEntity> Collection { get; set; }
        
        protected IReadService<TEntity> ReadService { get; set; }
        
        protected IWriteService<TEntity> WriteService { get; set; }

        public ScenarioBase()
        {
            TestConfig.Configure();
            Dependencies.Configure();
            
            var context = Dependencies.GetRequiredService<IMongoContext>();
            this.Collection = context.GetCollection<TEntity>();

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