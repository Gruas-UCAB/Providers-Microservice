using ProvidersMicroservice.src.provider.domain.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.value_objects
{
    public class ProviderId : IValueObject<ProviderId>
    {
        private readonly string _id;

        public ProviderId(string id)
        {
            if (!UUIDValidator.IsValid(id))
            {
                throw new InvalidProviderIdException();
            }
            _id = id;
        }

        public string GetId()
        {
            return _id;
        }

        public bool Equals(ProviderId other)
        {
            return _id == other.GetId();
        }
    }
}
