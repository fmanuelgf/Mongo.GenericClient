namespace Mongo.Generics.Repositories
{
    using System;
    using Mongo.Generics.Core.Attributes;
    using Mongo.Generics.Core.Entities;
    using Mongo.Generics.Core.Repositories;
    using MongoDB.Driver;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        public GenericRepository()
        {
            var client = new MongoClient(AppConfig.ConnectionString);
            var database = client.GetDatabase(AppConfig.DatabaseName);

            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            this.Collection = database.GetCollection<TEntity>(collectionName?.Name);
        }

        public IMongoCollection<TEntity> Collection { get; private set; }
    }
}