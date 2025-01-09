using ProvidersMicroservice.core.Common;
using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects
{
    public class ConductorLocation : IValueObject<ConductorLocation>
    {
        private readonly string _location;

        public ConductorLocation(string location)
        {
            if (!LocationValidator.IsValid(location))
            {
                throw new InvalidConductorLocationException(); 
            }
            _location = location;
        }

        public string GetLocation()
        {
            return _location;
        }
        public bool Equals(ConductorLocation other)
        {
            return _location == other.GetLocation();
        }
    }
}
