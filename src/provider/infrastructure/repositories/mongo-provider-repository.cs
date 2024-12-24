using MongoDB.Bson;
using MongoDB.Driver;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.infrastructure.models;
using UsersMicroservice.core.Common;
using UsersMicroservice.core.Infrastructure;

namespace ProvidersMicroservice.src.provider.infrastructure.repositories
{
    public class MongoProviderRepository : IProviderRepository
    {
        internal MongoDBConfig _config = new();
        private readonly IMongoCollection<BsonDocument> _providerCollection;
        private readonly ICraneRepository _craneRepository = new MongoCraneRepository();

        public MongoProviderRepository()
        {
            _providerCollection = _config.db.GetCollection<BsonDocument>("providers");
        }

        public async Task<_Optional<List<Conductor>>> GetAllConductors(GetAllConductorsDto data, ProviderId providerId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", providerId.GetId());
            var bsonProvider = await _providerCollection.Find(filter).FirstOrDefaultAsync();

            if (bsonProvider == null)
            {
                return _Optional<List<Conductor>>.Empty();
            }

            var bsonConductors = bsonProvider["conductors"].AsBsonArray;
            var conductors = new List<Conductor>();
            foreach(var bsonConductor in bsonConductors)
            {
                var conductor = new Conductor(
                    new ConductorId(bsonConductor["id"].AsString),
                    new ConductorDni(bsonConductor["dni"].AsInt32),
                    new ConductorName(bsonConductor["name"].AsString),
                    new ConductorImage(bsonConductor["image"].AsString),
                    new CraneId(bsonConductor["crane"].AsString)
                );

                if (!bsonConductor["isActive"].AsBoolean)
                {
                    conductor.ChangeStatus();
                }
                conductors.Add(conductor);
            }
            if (conductors.Count == 0)
            {
                return _Optional<List<Conductor>>.Empty();
            }
            return _Optional<List<Conductor>>.Of(conductors);
        }

        public async Task<_Optional<List<Provider>>> GetAllProviders(GetAllProvidersDto data)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isActive", data.active);
            var providers = await _providerCollection.Find(filter).Limit(data.limit).Skip(data.limit * (data.offset - 1)).ToListAsync();

            var providerList = new List<Provider>();

            foreach (var bsonProvider in providers)
            {
                var bsonConductors = bsonProvider["conductors"].AsBsonArray;
                var conductors = new List<Conductor>();
                foreach (var bsonConductor in bsonConductors)
                {
                    var conductor = new Conductor(
                        new ConductorId(bsonConductor["id"].AsString),
                        new ConductorDni(bsonConductor["dni"].AsInt32),
                        new ConductorName(bsonConductor["name"].AsString),
                        new ConductorImage(bsonConductor["image"].AsString),
                        new CraneId(bsonConductor["crane"].AsString)
                    );

                    if (!bsonConductor["isActive"].AsBoolean)
                    {
                        conductor.ChangeStatus();
                    }
                    conductors.Add(conductor);
                }

                var bsonCranes = bsonProvider["cranes"].AsBsonArray;
                var cranes = new List<Crane>();
                foreach (var bsonCrane in bsonCranes)
                {
                    var crane = new Crane(
                        new CraneId(bsonCrane["id"].AsString),
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
                    cranes.Add(crane);
                }

                var provider = Provider.Create(
                    new ProviderId(bsonProvider["_id"].AsString),
                    new ProviderName(bsonProvider["name"].AsString),
                    new ProviderRif(bsonProvider["rif"].AsString),
                    new ProviderImage(bsonProvider["image"].AsString),
                    conductors,
                    cranes
                );

                if (!bsonProvider["isActive"].AsBoolean)
                {
                    provider.ChangeStatus();
                }
                providerList.Add(provider);
            }
            if (providerList.Count == 0)
            {
                return _Optional<List<Provider>>.Empty();
            }
            return _Optional<List<Provider>>.Of(providerList);
        }


        public async Task<_Optional<Conductor>> GetConductorById(GetConductorByIdDto data)
        {  
            var providerFind = await GetProviderById(data.providerId);
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            var provider = providerFind.Unwrap();
            var conductor = provider.GetConductors().Find(c => c.Equals(data.conductorId));
            if (conductor == null)
            {
                return _Optional<Conductor>.Empty();
            }
                
            return _Optional<Conductor>.Of(conductor);                  
        }

        public async Task<_Optional<Provider>> GetProviderById(ProviderId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
                var bsonProvider = await _providerCollection.Find(filter).FirstOrDefaultAsync();

                if (bsonProvider == null)
                {
                    return _Optional<Provider>.Empty();
                }

                var bsonConductors = bsonProvider["conductors"].AsBsonArray;
                var conductors = new List<Conductor>();
                foreach (var bsonConductor in bsonConductors)
                {
                    var conductor = new Conductor(
                        new ConductorId(bsonConductor["id"].AsString),
                        new ConductorDni(bsonConductor["dni"].AsInt32),
                        new ConductorName(bsonConductor["name"].AsString),
                        new ConductorImage(bsonConductor["image"].AsString),
                        new CraneId(bsonConductor["crane"].AsString)
                    );

                    if (!bsonConductor["isActive"].AsBoolean)
                    {
                        conductor.ChangeStatus();
                    }
                    conductors.Add(conductor);
                }
                var bsonCranes = bsonProvider["cranes"].AsBsonArray;
                var cranes = new List<Crane>();
                foreach (var bsonCrane in bsonCranes)
                {
                    var crane = new Crane(
                        new CraneId(bsonCrane["id"].AsString),
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
                    cranes.Add(crane);
                }

                var provider = Provider.Create(
                    new ProviderId(bsonProvider["_id"].AsString),
                    new ProviderName(bsonProvider["name"].AsString),
                    new ProviderRif(bsonProvider["rif"].AsString),
                    new ProviderImage(bsonProvider["image"].AsString),
                    conductors,
                    cranes
                );

                Console.WriteLine(provider.GetConductors().Count());
                Console.WriteLine(provider.GetCranes().Count());

                if (!bsonProvider["isActive"].AsBoolean)
                {
                    provider.ChangeStatus();
                }
                return _Optional<Provider>.Of(provider);
            }
            catch (Exception e)
            {
                return _Optional<Provider>.Empty();
            }
        }

        public async Task<Conductor> SaveConductor(SaveConductorDto data)
        {
            var providerFind = await GetProviderById(data.providerId);
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            //var craneFind = await GetCraneById(new GetCraneByIdDto(data.providerId, new CraneId(data.conductor.GetAssignedCrane())));
            //if (!craneFind.HasValue())
            //{
            //    throw new CraneNotFoundException();
            //}
            var provider = providerFind.Unwrap();
            var conductorAdded = provider.AddConductor(
                    new ConductorId(data.conductor.GetId()),
                    new ConductorDni(data.conductor.GetDni()),
                    new ConductorName(data.conductor.GetName()),
                    new ConductorImage(data.conductor.GetImage()),
                    new CraneId(data.conductor.GetAssignedCrane())
                );
            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var conductorsBsonArray = new BsonArray();
            foreach (var c in provider.GetConductors())
            {
                var conductorBson = new BsonDocument
                {
                    { "id", c.GetId() },
                    { "dni", c.GetDni() },
                    { "name", c.GetName() },
                    { "image", c.GetImage() },
                    { "crane", c.GetAssignedCrane() },
                    { "isActive", c.IsActive() }
                };
                conductorsBsonArray.Add(conductorBson);
            }

            var update = Builders<BsonDocument>.Update
                .Set("conductors", conductorsBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);
            return conductorAdded;
        }

        public async Task<Provider> SaveProvider(Provider provider)
        {
            var mongoProvider = new MongoProvider
            {
                Id = provider.GetId(),
                Rif = provider.GetRif(),
                Name = provider.GetName(),
                Image = provider.GetImage(),
                Cranes = new List<Crane>(),
                Conductors = new List<Conductor>(),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var bsonDocument = new BsonDocument
            {
                {"_id", mongoProvider.Id},
                {"rif", mongoProvider.Rif},
                {"name", mongoProvider.Name},
                {"image", mongoProvider.Image},
                {"cranes", new BsonArray()},
                {"conductors", new BsonArray()},
                {"isActive", mongoProvider.IsActive},
                {"createdAt", mongoProvider.CreatedAt},
                {"updatedAt", mongoProvider.UpdatedAt}
            };

            await _providerCollection.InsertOneAsync(bsonDocument);

            var savedProvider = Provider.Create(
                new ProviderId(mongoProvider.Id),
                new ProviderName(mongoProvider.Name),
                new ProviderRif(mongoProvider.Rif),
                new ProviderImage(mongoProvider.Image),
                new List<Conductor>(),
                new List<Crane>()
            );

            return savedProvider;
        }

        public async Task<ConductorId> ToggleActivityConductorById(ToggleActivityConductorByIdDto data)
        {
            var conductor = await GetConductorById(new GetConductorByIdDto(data.providerId, data.conductorId));
            if (!conductor.HasValue())
            {
                throw new ConductorNotFoundException();
            }

            var conductorToUpdate = conductor.Unwrap();
            conductorToUpdate.ChangeStatus();
            var conductorsFind = await GetAllConductors(new GetAllConductorsDto(), data.providerId);
            var conductors = conductorsFind.Unwrap();
            conductors.ForEach(c =>
            {
                if (c.GetId() == conductorToUpdate.GetId())
                {
                    c.ChangeStatus();
                }
            });

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("conductors", conductors)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);
            return data.conductorId;
        }

        public async Task<ProviderId> ToggleActivityProviderById(ProviderId id)
        {
            var provider = await GetProviderById(id);
            if (!provider.HasValue())
            {
                throw new ProviderNotFoundException();
            }

            var providerToUpdate = provider.Unwrap();
            providerToUpdate.ChangeStatus();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("isActive", providerToUpdate.IsActive())
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);
            return id;
        }

        public async Task<Crane> SaveCrane(SaveCraneDto data)
        {
            var providerFind = await GetProviderById(data.providerId);
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            
            var provider = providerFind.Unwrap();
            var craneAdded = provider.AddCrane(
                    new CraneId(data.crane.GetId()),
                    new CraneBrand(data.crane.GetBrand()),
                    new CraneModel(data.crane.GetModel()),
                    new CranePlate(data.crane.GetPlate()),
                    new CraneType(data.crane.GetType()),
                    new CraneYear(data.crane.GetYear())
                );

            var cranesBsonArray = new BsonArray();
            foreach (var c in provider.GetCranes())
            {
                var craneBson = new BsonDocument
                {
                    { "id", c.GetId() },
                    { "brand", c.GetBrand() },
                    { "model", c.GetModel() },
                    { "plate", c.GetPlate() },
                    { "type", c.GetType() },
                    { "year", c.GetYear() },
                    { "isActive", c.IsActive() }
                };
                cranesBsonArray.Add(craneBson);
            }

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("cranes", cranesBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);
            return craneAdded;
        }

        public async Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data, ProviderId providerId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", providerId.GetId());
            var bsonProvider = await _providerCollection.Find(filter).FirstOrDefaultAsync();

            if (bsonProvider == null)
            {
                return _Optional<List<Crane>>.Empty();
            }

            var bsonConductors = bsonProvider["cranes"].AsBsonArray;
            var cranes = new List<Crane>();
            foreach (var bsonCrane in bsonConductors)
            {
                var crane = new Crane(
                    new CraneId(bsonCrane["id"].AsString),
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
                cranes.Add(crane);
            }
            return (cranes.Count == 0) ? _Optional<List<Crane>>.Empty() : _Optional<List<Crane>>.Of(cranes);
        }

        public async Task<_Optional<Crane>> GetCraneById(GetCraneByIdDto data)
        {
            var providerFind = await GetProviderById(new ProviderId(data.providerId));
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            var provider = providerFind.Unwrap();
            var crane = provider.GetCranes().Find(c => c.Equals(new CraneId(data.craneId)));
            if (crane == null)
            {
                return _Optional<Crane>.Empty();
            }
            return _Optional<Crane>.Of(crane);
        }

        public async Task<CraneId> ToggleActivityCraneById(ToggleActivityCraneByIdDto data)
        {
            var crane = await GetCraneById(new GetCraneByIdDto(data.providerId.GetId(), data.craneId.GetId()));
            if (!crane.HasValue())
            {
                throw new ConductorNotFoundException();
            }

            var craneToUpdate = crane.Unwrap();
            craneToUpdate.ChangeStatus();
            var cranesFind = await GetAllCranes(new GetAllCranesDto(), (data.providerId));
            var cranes = cranesFind.Unwrap();
            cranes.ForEach(c =>
            {
                if (c.GetId() == craneToUpdate.GetId())
                {
                    c.ChangeStatus();
                }
            });

            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("cranes", cranes)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);
            return data.craneId;
        }
        public async Task<ConductorId> AssignCraneToConductorById(AssignCraneToConductorDto data)
        {
            var providerFind = await GetProviderById(data.providerId);
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            var conductorFind = await GetConductorById(new GetConductorByIdDto(data.providerId, data.conductorId));
            if (!conductorFind.HasValue())
            {
                throw new ConductorNotFoundException();
            }
            var craneFind = await GetCraneById(new GetCraneByIdDto(data.providerId.GetId(), data.craneId.GetId()));
            if (!craneFind.HasValue())
            {
                throw new CraneNotFoundException();
            }
            var provider = providerFind.Unwrap();
            var conductor = conductorFind.Unwrap();
            var crane = craneFind.Unwrap();

            provider.AssignCraneToConductor(crane.Id, conductor);

            var conductorsBsonArray = new BsonArray();
            foreach (var c in provider.GetConductors())
            {
                var conductorBson = new BsonDocument
                {
                    { "id", c.GetId() },
                    { "dni", c.GetDni() },
                    { "name", c.GetName() },
                    { "image", c.GetImage() },
                    { "crane", c.GetAssignedCrane() },
                    { "isActive", c.IsActive() }
                };
                conductorsBsonArray.Add(conductorBson);
            }
            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("conductors", conductorsBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);

            return data.conductorId;
        }

        public async Task<ConductorId> UnassignCraneFromConductorById(UnassignCraneToConductorDto data)
        {
            var providerFind = await GetProviderById(data.providerId);
            if (!providerFind.HasValue())
            {
                throw new ProviderNotFoundException();
            }
            var conductorFind = await GetConductorById(new GetConductorByIdDto(data.providerId, data.conductorId));
            if (!conductorFind.HasValue())
            {
                throw new ConductorNotFoundException();
            }
            var craneFind = await GetCraneById(new GetCraneByIdDto(data.providerId.GetId(), data.craneId.GetId()));
            if (!craneFind.HasValue())
            {
                throw new CraneNotFoundException();
            }
            var provider = providerFind.Unwrap();
            var conductor = conductorFind.Unwrap();
            var crane = craneFind.Unwrap();

            provider.RemoveCraneFromConductor(conductor, crane.Id);

            var conductorsBsonArray = new BsonArray();
            foreach (var c in provider.GetConductors())
            {
                var conductorBson = new BsonDocument
                {
                    { "id", c.GetId() },
                    { "dni", c.GetDni() },
                    { "name", c.GetName() },
                    { "image", c.GetImage() },
                    { "crane", c.GetAssignedCrane() },
                    { "isActive", c.IsActive() }
                };
                conductorsBsonArray.Add(conductorBson);
            }
            var filter = Builders<BsonDocument>.Filter.Eq("_id", data.providerId.GetId());
            var update = Builders<BsonDocument>.Update
                .Set("conductors", conductorsBsonArray)
                .Set("updatedAt", DateTime.Now);

            await _providerCollection.UpdateOneAsync(filter, update);

            return data.conductorId;
        }
    }
}
