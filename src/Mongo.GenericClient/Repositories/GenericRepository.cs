namespace Mongo.GenericClient.Repositories
{
    using System;
    using Mongo.GenericClient.Core.Attributes;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Repositories;
    using MongoDB.Driver;

    [Obsolete("Use IMongoContext instead as this class will be removed.")]
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        public GenericRepository()
        {
            var collectionName =
                Attribute.GetCustomAttribute(
                    typeof(TEntity),
                    typeof(CollectionNameAttribute)) as CollectionNameAttribute;

            this.Collection = MongoHelper.Database.GetCollection<TEntity>(collectionName?.Name);
        }

        /// <inheritdoc />
        [Obsolete("Use IMongoContext.GetCollection<TEntity>() instead as this class will be removed.")]
        public IMongoCollection<TEntity> Collection { get; private set; }
    }
}