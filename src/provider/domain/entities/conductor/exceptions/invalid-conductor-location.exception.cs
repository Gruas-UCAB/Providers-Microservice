using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class InvalidConductorLocationException : DomainException
    {
        public InvalidConductorLocationException() : base("Invalid conductor location.")
        {
        }
    }
}
