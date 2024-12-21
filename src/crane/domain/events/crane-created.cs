using ProvidersMicroservice.src.crane.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.events
{
    public class CraneCreatedEvent : DomainEvent<object>
    {
        public CraneCreatedEvent(string dispatcherId, string name, CraneCreated context) : base(dispatcherId, name, context){ }
    }

    public class CraneCreated(string Brand, string Model, string Plate, string Type, int Year)
    {
        public string Brand = Brand;
        public string Model = Model;
        public string Plate = Plate;
        public string Type = Type;
        public int Year = Year;

        public static CraneCreatedEvent CreateEvent(CraneId DispatcherId, CraneBrand Brand, CraneModel Model, CranePlate Plate, CraneType Type, CraneYear Year)
        {
            return new CraneCreatedEvent(
                DispatcherId.GetId(),
                typeof(CraneCreated).Name,
                new CraneCreated(
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
