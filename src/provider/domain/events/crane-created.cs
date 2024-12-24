using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.events
{
    public class CraneCreatedEvent : DomainEvent<object>
    {
        public CraneCreatedEvent(string dispatcherId, string name, CraneCreated context) : base(dispatcherId, name, context) { }
    }

    public class CraneCreated(string Id, string Brand, string Model, string Plate, string Type, int Year)
    {
        public string Id = Id;
        public string Brand = Brand;
        public string Model = Model;
        public string Plate = Plate;
        public string Type = Type;
        public int Year = Year;

        public static CraneCreatedEvent CreateEvent(ProviderId DispatcherId, CraneId Id, CraneBrand Brand, CraneModel Model, CranePlate Plate, CraneType Type, CraneYear Year)
        {
            return new CraneCreatedEvent(
                DispatcherId.GetId(),
                typeof(CraneCreated).Name,
                new CraneCreated(
                    Id.GetId(),
                    Brand.GetBrand(),
                    Model.GetModel(),
                    Plate.GetPlate(),
                    Type.GetType(),
                    Year.GetYear()
                )
            );
        }

    }
}
