using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.events
{
    public class ConductorCreatedEvent : DomainEvent<object>
    {
        public ConductorCreatedEvent(string dispatcherId, string name, ConductorCreated context) : base(dispatcherId, name, context) { }
    }

    public class ConductorCreated(string Id, int Dni, string Name, string Image, string? CraneId)
    {
        public string Id = Id;
        public int Dni = Dni;
        public string Name = Name;
        public string Image = Image;
        public string? CraneId = CraneId;

        public static ConductorCreatedEvent CreateEvent(ProviderId DispatcherId, ConductorId Id, ConductorDni Dni, ConductorName Name, ConductorImage Image, CraneId? AssignedCrane)
        {
            return new ConductorCreatedEvent(
                DispatcherId.GetId(),
                typeof(ConductorCreated).Name,
                new ConductorCreated(
                    Id.GetId(),
                    Dni.GetDni(),
                    Name.GetName(),
                    Image.GetImage(),
                    AssignedCrane?.GetId()
                )
            );
        }

    }
}
