using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.crane;

namespace ProvidersMicroservice.src.provider.infrastructure.models
{
    public class MongoProvider
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("rif"), BsonRepresentation(BsonType.String)]
        public required string Rif { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public required string Name { get; set; }

        [BsonElement("image"), BsonRepresentation(BsonType.String)]
        public required string Image { get; set; }

        [BsonElement("cranes")]
        public required List<Crane> Cranes { get; set; }

        [BsonElement("conductors")]
        public required List<Conductor> Conductors { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
