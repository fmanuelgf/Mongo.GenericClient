namespace Mongo.GenericClient.Core.Repositories
{
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    [Obsolete("Use IMongoContext instead as this interface will be removed.")]
    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// The collection of type <see cref="TEntity"/>.
        /// </summary>
        [Obsolete("Use IMongoContext.GetCollection<TEntity>() instead as this interface will be removed.")]
        IMongoCollection<TEntity> Collection { get; }
    }
}