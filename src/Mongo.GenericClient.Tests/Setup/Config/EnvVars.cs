namespace Mongo.GenericClient.Tests.Setup.Config
{
    using System;

    internal static class EnvVars
    { 
        internal static void Configure()
        {
            Environment.SetEnvironmentVariable("MONGODB_CONNECTION_STRING", "mongodb://root:root@localhost:27017");
            Environment.SetEnvironmentVariable("MONGODB_DATABASE_NAME", "test_db");
        }
    }
}