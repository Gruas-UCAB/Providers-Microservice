using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CraneId : IValueObject<CraneId>
    {
        private readonly string _id;

        public CraneId(string id)
        {
            if (UUIDValidator.IsValid(id))
            {
                _id = id;
            }
            else if(id == null)
            {
                return;
            }
            else
            {
                throw new InvalidCraneIdException(); ;
            }

        }

        public string GetId()
        {
            return _id;
        }
        public bool Equals(CraneId other)
        {
            return this._id == other.GetId();
        }
    }
}
