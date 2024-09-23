namespace Mongo.Generics.Core.Repositories
{
    using Mongo.Generics.Core.Entities;
    using MongoDB.Driver;

    public interface IGenericRepository<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        IMongoCollection<TEntity> Collection { get; }
    }
}