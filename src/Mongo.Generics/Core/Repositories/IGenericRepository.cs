namespace Mongo.Generics.Core.Repositories
{
    using Mongo.Generics.Core.Entities;
    using MongoDB.Driver;

    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// The collection of <see cref="TEntity"/>.
        /// </summary>
        IMongoCollection<TEntity> Collection { get; }
    }
}