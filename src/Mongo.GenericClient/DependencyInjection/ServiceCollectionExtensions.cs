namespace Mongo.GenericClient.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Mongo.GenericClient.Core;
    using Mongo.GenericClient.Core.Entities;
    using Mongo.GenericClient.Core.Services;
    using Mongo.GenericClient.Services;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register IMongoContext and its implementation.
        /// </summary>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterMongoContext(
            this IServiceCollection services,
            RegisterMode registerMode = RegisterMode.Singleton)
        {
            switch (registerMode)
            {
                case RegisterMode.Transient:
                    services.AddTransient<IMongoContext, MongoContext>();
                    break;

                case RegisterMode.Scoped:
                    services.AddScoped<IMongoContext, MongoContext>();
                    break;

                default:
                    services.AddSingleton<IMongoContext, MongoContext>();
                    break;
            }
        }

        /// <summary>
        /// Register IReadService, IWriteService and their implementations for the given Entity.
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="TEntity"/>.</typeparam>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterGenericServices<TEntity>(
            this IServiceCollection services,
            RegisterMode registerMode = RegisterMode.Singleton) where TEntity : IEntity
        {
            switch (registerMode)
            {
                case RegisterMode.Transient:
                    services.AddTransient<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddTransient<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;

                case RegisterMode.Scoped:
                    services.AddScoped<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddScoped<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;

                default:
                    services.AddSingleton<IReadService<TEntity>, ReadService<TEntity>>();
                    services.AddSingleton<IWriteService<TEntity>, WriteService<TEntity>>();
                    break;
            }
        }

        /// <summary>
        /// Register IReadService, IWriteService and their implementations for all the Entities.
        /// </summary>
        /// <param name="registerMode">The <see cref="RegisterMode"> (Transient, Scoped, Singleton).</param>
        public static void RegisterAllGenericServices(
            this IServiceCollection services,
            RegisterMode registerMode = RegisterMode.Singleton)
        {
            switch (registerMode)
            {
                case RegisterMode.Transient:
                    services.AddTransient(typeof(IReadService<>), typeof(ReadService<>));
                    services.AddTransient(typeof(IWriteService<>), typeof(WriteService<>));
                    break;

                case RegisterMode.Scoped:
                    services.AddScoped(typeof(IReadService<>), typeof(ReadService<>));
                    services.AddScoped(typeof(IWriteService<>), typeof(WriteService<>));
                    break;

                default:
                    services.AddSingleton(typeof(IReadService<>), typeof(ReadService<>));
                    services.AddSingleton(typeof(IWriteService<>), typeof(WriteService<>));
                    break;
            }
        }
    }
}