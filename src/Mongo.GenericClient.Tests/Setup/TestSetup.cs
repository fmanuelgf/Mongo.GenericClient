namespace Mongo.GenericClient.Tests.Setup
{
    using Mongo.GenericClient.Tests.Setup.Config;

    internal static class TestSetup
    {
        internal static Dependencies Dependencies = new();

        internal static void Configure()
        {
            EnvVars.Configure();
        }
    }
}