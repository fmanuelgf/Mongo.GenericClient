namespace Mongo.GenericClient
{
    public static class AppConfig
    {
        public static string ConnectionString => Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")
            ?? string.Empty;

        public static string DatabaseName => Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME")
            ?? string.Empty;
    }
}