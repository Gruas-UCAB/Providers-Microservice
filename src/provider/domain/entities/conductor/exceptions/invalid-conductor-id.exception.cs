using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class InvalidConductorIdException : DomainException
    {
        public InvalidConductorIdException() : base("Invalid conductor id.")
        { }
    }
}
