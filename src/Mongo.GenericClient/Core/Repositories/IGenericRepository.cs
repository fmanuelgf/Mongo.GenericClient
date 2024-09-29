namespace Mongo.GenericClient.Core.Repositories
{
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// The collection of type <see cref="TEntity"/>.
        /// </summary>
        IMongoCollection<TEntity> Collection { get; }
    }
}