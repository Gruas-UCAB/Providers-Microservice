using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProvidersMicroservice.src.provider.infrastructure.models
{
    public class MongoConductor
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("dni"), BsonRepresentation(BsonType.Int32)]
        public required int Dni { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public required string Name { get; set; }

        [BsonElement("location"), BsonRepresentation(BsonType.String)]
        public required string Location { get; set; }

        [BsonElement("image"), BsonRepresentation(BsonType.String)]
        public required string Image { get; set; }

        [BsonElement("craneAssigned"), BsonRepresentation(BsonType.String)]
        public string? CraneAssigned { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
