using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class InvalidConductorDniException : DomainException
    {
        public InvalidConductorDniException() : base("Invalid conductor dni.")
        { }
    }
}
