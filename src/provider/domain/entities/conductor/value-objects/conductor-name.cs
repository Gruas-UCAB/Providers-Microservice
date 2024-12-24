using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects
{
    public class ConductorName : IValueObject<ConductorName>
    {
        private string _name;

        public ConductorName(string name) 
        {
            if (name.Length < 3 || name.Length > 30 )
            {
                throw new InvalidConductorNameException();
            }
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
        public bool Equals(ConductorName other)
        {
            return _name == other.GetName();
        }
    }
}
