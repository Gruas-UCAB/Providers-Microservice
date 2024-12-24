using ProvidersMicroservice.src.provider.domain.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.value_objects
{
    public class ProviderName : IValueObject<ProviderName>
    {
        private string _name;

        public ProviderName(string name)
        {
            if (name.Length < 2 || name.Length > 25)
            {
                throw new InvalidProviderNameException();
            }
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
        public bool Equals(ProviderName other)
        {
            return _name == other.GetName();
        }
    }
}
