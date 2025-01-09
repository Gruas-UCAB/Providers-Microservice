using MongoDB.Bson;
using MongoDB.Driver;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.infrastructure.models;
using UsersMicroservice.core.Common;
using UsersMicroservice.core.Infrastructure;

namespace ProvidersMicroservice.src.provider.infrastructure.repositories
{
    public class MongoCraneRepository : ICraneRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> craneCollection;

        public MongoCraneRepository()
        {
            craneCollection = _config.db.GetCollection<BsonDocument>("cranes");
        }
        public async Task<Crane> SaveCrane(Crane crane)
        {
            var mongoCrane = new MongoCrane
            {
                Id = crane.GetId(),
                Brand = crane.GetBrand(),
                Model = crane.GetModel(),
                Plate = crane.GetPlate(),
                Type = crane.GetType(),
                Year = crane.GetYear(),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoCrane.Id},
                {"brand", mongoCrane.Brand},
                {"model", mongoCrane.Model},
                {"plate", mongoCrane.Plate},
                {"type", mongoCrane.Type},
                {"year", mongoCrane.Year},
                {"isActive", mongoCrane.IsActive},
                {"createdAt", mongoCrane.CreatedAt},
                {"updatedAt", mongoCrane.UpdatedAt}
            };

            await craneCollection.InsertOneAsync(bsonDocument);

            var savedCrane = new Crane(
                new CraneId(mongoCrane.Id),
                new CraneBrand(mongoCrane.Brand),
                new CraneModel(mongoCrane.Model),
                new CranePlate(mongoCrane.Plate),
                new CraneType(mongoCrane.Type),
                new CraneYear(mongoCrane.Year)
            );

            return savedCrane;
        }
        public async Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var cranes = await craneCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();

            var craneList = new List<Crane>();

            foreach (var bsonCrane in cranes)
            {
                var crane = new Crane(
                new CraneId(bsonCrane["_id"].AsString),
                new CraneBrand(bsonCrane["brand"].AsString),
                new CraneModel(bsonCrane["model"].AsString),
                new CranePlate(bsonCrane["plate"].AsString),
                new CraneType(bsonCrane["type"].AsString),
                new CraneYear(bsonCrane["year"].AsInt32)
                );

                if (!bsonCrane["isActive"].AsBoolean)
                {
                    crane.ChangeStatus();
                }
                craneList.Add(crane);
            }

            if (craneList.Count == 0)
            {
                return _Optional<List<Crane>>.Empty();
            }
            return _Optional<List<Crane>>.Of(craneList);
        }
        public async Task<_Optional<Crane>> GetCraneById(CraneId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonCrane = await craneCollection.Find(filter).FirstOrDefaultAsync();

                if (bsonCrane == null)
                {
                    return _Optional<Crane>.Empty();
                }

                var crane = new Crane(
                new CraneId(bsonCrane["_id"].AsString),
                new CraneBrand(bsonCrane["brand"].AsString),
                new CraneModel(bsonCrane["model"].AsString),
                new CranePlate(bsonCrane["plate"].AsString),
                new CraneType(bsonCrane["type"].AsString),
                new CraneYear(bsonCrane["year"].AsInt32)
                );

                if (!bsonCrane["isActive"].AsBoolean)
                {
                    crane.ChangeStatus();
                }
                return _Optional<Crane>.Of(crane);
            }
            catch (Exception e)
            {
                return _Optional<Crane>.Empty();
            }
        }
        public async Task<CraneId> ToggleActivityCraneById(CraneId id)
        {
            var crane = await GetCraneById(id);
            if (!crane.HasValue())
            {
                throw new CraneNotFoundException();
            }

            var craneToUpdate = crane.Unwrap();
            craneToUpdate.ChangeStatus();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("isActive", craneToUpdate.IsActive())
                .Set("updatedAt", DateTime.Now);

            await craneCollection.UpdateOneAsync(filter, update);
            return id;
        }
    }
}
