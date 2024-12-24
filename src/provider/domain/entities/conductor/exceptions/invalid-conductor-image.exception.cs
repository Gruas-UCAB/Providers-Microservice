using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class InvalidConductorImageException : DomainException
    {
        public InvalidConductorImageException() : base("Invalid conductor image.")
        { }
    }
}
