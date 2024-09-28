namespace Mongo.GenericClient.Tests.IoC
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Mongo.GenericClient.DependencyInjection;
    using Mongo.GenericClient.Tests.SetUp;

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
            services.ResisterGenericRepository<PersonEntity>(RegisterMode.Transient);
            services.ResisterGenericServices<PersonEntity>(RegisterMode.Transient);

            provider = services.BuildServiceProvider();
        }
    }
}