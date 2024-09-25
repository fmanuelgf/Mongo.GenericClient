namespace Mongo.Generics.Core.Entities
{
    using MongoDB.Bson;

    public interface IEntity
    {
        /// <summary>
        /// The entity ID
        /// </summary>
        ObjectId Id { get; set; }
    }
}