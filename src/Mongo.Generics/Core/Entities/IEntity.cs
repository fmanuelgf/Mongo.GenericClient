namespace Mongo.Generics.Core.Entities
{
    using MongoDB.Bson;

    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}