namespace Mongo.Generics.Core.Entities
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public abstract class AuditableEntity
    {
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("deletedAt")]
        public DateTime? DeletedAt { get; set; }
    }
}