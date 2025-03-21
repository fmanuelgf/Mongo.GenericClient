namespace Mongo.GenericClient.Core
{
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public interface IMongoContext
    {
        /// <summary>
        /// Get the collection of type <see cref="TEntity"/>.
        /// </summary>
        /// <returns>The <see cref="IMongoCollection"/>.</returns>
        IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity;
    }
}