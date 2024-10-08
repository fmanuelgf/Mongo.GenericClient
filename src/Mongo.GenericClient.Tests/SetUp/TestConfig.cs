namespace Mongo.GenericClient.Tests.Setup
{
    using System;

    public static class TestConfig
    { 
        public static void Configure()
        {
            Environment.SetEnvironmentVariable("MONGODB_CONNECTION_STRING", "mongodb://root:root@localhost:27017");
            Environment.SetEnvironmentVariable("MONGODB_DATABASE_NAME", "test_db");
        }
    }
}