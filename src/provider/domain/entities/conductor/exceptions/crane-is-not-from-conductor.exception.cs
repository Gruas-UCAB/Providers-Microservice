using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class CraneIsNotFromConductorException : DomainException
    {
        public CraneIsNotFromConductorException() : base("The crane is not from the conductor")
        {
        }
    }
}
