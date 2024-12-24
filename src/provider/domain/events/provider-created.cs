using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.events
{
    public class ProviderCreatedEvent : DomainEvent<object>
    {
        public ProviderCreatedEvent(string dispatcherId, string name, ProviderCreated context) : base(dispatcherId, name, context) { }
    }

    public class ProviderCreated(string Rif, string Name, string Image, List<Conductor> conductors, List<Crane> cranes)
    {
        public string Rif = Rif;
        public string Name = Name;
        public string Image = Image;
        public List<Conductor> Conductors = conductors;
        public List<Crane> Cranes = cranes;

        public static ProviderCreatedEvent CreateEvent(ProviderId DispatcherId, ProviderRif Rif, ProviderName Name, ProviderImage Image, List<Conductor> Conductors, List<Crane> Cranes)
        {
            return new ProviderCreatedEvent(
                DispatcherId.GetId(),
                typeof(ProviderCreated).Name,
                new ProviderCreated(
                    Rif.GetRif(),
                    Name.GetName(),
                    Image.GetImage(),
                    Conductors,
                    Cranes
                )
            );
        }

    }

}
