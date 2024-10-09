namespace Mongo.GenericClient.Tests.Setup
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Mongo.GenericClient.DependencyInjection;

    public static class Dependencies
    {
        private static ServiceCollection services = new();
        private static IServiceProvider provider = services.BuildServiceProvider();

        public static T GetRequiredService<T>() where T : notnull
        {
            return (T)provider.GetRequiredService(typeof(T));
        }
        
        public static void Configure()
        {
            services = new ServiceCollection();
            services.RegisterMongoContext(RegisterMode.Singleton);
            services.RegisterGenericServices<PersonEntity>(RegisterMode.Transient);

            provider = services.BuildServiceProvider();
        }
    }
}