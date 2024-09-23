namespace Mongo.Generics.Core.Services
{
    using System.Threading.Tasks;
    using Mongo.Generics.Core.Entities;

    public interface IWriteService<TEntity>
        where TEntity : AuditableEntity, IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
    }
}