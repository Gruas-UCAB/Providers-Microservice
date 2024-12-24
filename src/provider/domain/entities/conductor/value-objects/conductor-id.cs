using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects
{
    public class ConductorId : IValueObject<ConductorId>
    {
        private readonly string _id;
        public ConductorId(string id)
        {
            if (UUIDValidator.IsValid(id))
            {
                _id = id;
            }
            else
            {
                throw new InvalidConductorIdException( );
            }
        }

        public string GetId()
        {
            return _id;
        }

        public bool Equals(ConductorId other)
        {
            return this._id == other.GetId();
        }
    }
}