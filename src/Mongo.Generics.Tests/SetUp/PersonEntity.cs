namespace Mongo.Generics.Tests.SetUp
{
    using Mongo.Generics.Core.Attributes;
    using Mongo.Generics.Core.Entities;
    using MongoDB.Bson;

    [CollectionName("persons")]
    public class PersonEntity : IEntity
    {
        public ObjectId Id { get ; set;  }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}