namespace Mongo.GenericClient.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Repositories;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Repositories;
    using Mongo.GenericClient.Services;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register IReadService, IWriteService and their implementations for the given Entity.
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="TEntity"/>.</typeparam>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterGenericServices<TEntity>(this IServiceCollection services, RegisterMode registerMode)
            where TEntity : IEntity
        {
            switch (registerMode)
            {
                case RegisterMode.Scoped:
                    services.AddScoped<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddScoped<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;
                
                case RegisterMode.Singleton:
                    services.AddSingleton<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddSingleton<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;

                default:
                    services.AddTransient<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddTransient<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;
            }
        }

        /// <summary>
        /// Register IGenericRepository and its implementation for the given Entity.
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="TEntity"/>.</typeparam>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterGenericRepository<TEntity>(this IServiceCollection services, RegisterMode registerMode)
            where TEntity : IEntity
        {
            switch (registerMode)
            {
                case RegisterMode.Scoped:
                    services.AddScoped<IGenericRepository<TEntity>, GenericRepository<TEntity>>();
                    break;
                
                case RegisterMode.Singleton:
                    services.AddSingleton<IGenericRepository<TEntity>, GenericRepository<TEntity>>();
                    break;

                default:
                    services.AddTransient<IGenericRepository<TEntity>, GenericRepository<TEntity>>();
                    break;
            }
        }

        /// <summary>
        /// Register IGenericRepository, IReadService, IWriteService and their implementations for the given Entity.
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="TEntity"/>.</typeparam>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterGenericRepositoryAndServices<TEntity>(this IServiceCollection services, RegisterMode registerMode)
            where TEntity : IEntity
        {
            services.RegisterGenericRepository<TEntity>(registerMode);
            services.RegisterGenericServices<TEntity>(registerMode);
        }
    }
}