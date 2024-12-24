using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CraneType : IValueObject<CraneType>
    {
        private readonly string _type;
        private readonly List<string> _types = ["light", "medium", "heavy"];

        public CraneType(string type) 
        {
            if (!_types.Contains(type))
            {
                throw new InvalidCraneTypeException();
            }
            _type = type;
        }

        public string GetType()
        {
            return _type;
        }

        public bool Equals(CraneType other)
        {
            return _type == other._type;
        }
    }
}
