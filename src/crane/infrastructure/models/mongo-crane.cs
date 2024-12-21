using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProvidersMicroservice.src.crane.infrastructure.models
{
    public class MongoCrane
    {
        [BsonId]
        [BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("brand"), BsonRepresentation(BsonType.String)]
        public required string Brand { get; set; }

        [BsonElement("model"), BsonRepresentation(BsonType.String)]
        public required string Model { get; set; }

        [BsonElement("plate"), BsonRepresentation(BsonType.String)]
        public required string Plate { get; set; }

        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        public required string Type { get; set; }

        [BsonElement("year"), BsonRepresentation(BsonType.Int32)]
        public required int Year { get; set; }

        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public required bool IsActive { get; set; }

        [BsonElement("createdAt"), BsonRepresentation(BsonType.String)]
        public required DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt"), BsonRepresentation(BsonType.String)]
        public required DateTime UpdatedAt { get; set; }
    }
}
