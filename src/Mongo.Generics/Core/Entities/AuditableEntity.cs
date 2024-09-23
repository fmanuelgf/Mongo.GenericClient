namespace Mongo.Generics.Core.Entities
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public abstract class AuditableEntity
    {
        [BsonElement("crtdat")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updtat")]
        public DateTime? UpdateddAt { get; set; }

        [BsonElement("delat")]
        public DateTime? DeletedAt { get; set; }
    }
}