namespace Mongo.GenericClient.Tests.Base
{
    using System;
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Tests.Setup;
    using MongoDB.Driver;

    public abstract class StepDefinitions<TEntity> : IDisposable
        where TEntity : IEntity
    {
        public IMongoCollection<TEntity> Collection { get; set; }
        
        public IReadService<TEntity> ReadService { get; set; }
        
        public IWriteService<TEntity> WriteService { get; set; }

        public StepDefinitions()
        {
            TestSetup.Configure();
            
            var context = TestSetup.Dependencies.GetRequiredService<IMongoContext>();
            this.Collection = context.GetCollection<TEntity>();

            this.ReadService = TestSetup.Dependencies.GetRequiredService<IReadService<TEntity>>();
            this.WriteService = TestSetup.Dependencies.GetRequiredService<IWriteService<TEntity>>();
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