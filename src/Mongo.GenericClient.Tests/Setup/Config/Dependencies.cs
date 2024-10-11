namespace Mongo.GenericClient.Tests.Setup.Config
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Mongo.GenericClient.DependencyInjection;
    using Mongo.GenericClient.Tests.Setup.TestData;

    internal class Dependencies
    {
        private readonly ServiceCollection services;
        private readonly IServiceProvider provider ;

        internal Dependencies()
        {
            this.services = new ServiceCollection();
            this.services.RegisterMongoContext(RegisterMode.Singleton);
            this.services.RegisterGenericServices<PersonEntity>(RegisterMode.Transient);

            this.provider = this.services.BuildServiceProvider();
        }
        
        internal T GetRequiredService<T>() where T : notnull
        {
            return (T)this.provider.GetRequiredService(typeof(T));
        }
    }
}