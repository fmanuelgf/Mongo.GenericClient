namespace Mongo.GenericClient.Core
{
    using Mongo.GenericClient.Core.Entities;
    using MongoDB.Driver;

    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity;
    }
}