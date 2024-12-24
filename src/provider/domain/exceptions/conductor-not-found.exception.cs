using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class ConductorNotFoundException : DomainException
    {
        public ConductorNotFoundException() : base("Conductor not found")
        {
        }
    }
}
