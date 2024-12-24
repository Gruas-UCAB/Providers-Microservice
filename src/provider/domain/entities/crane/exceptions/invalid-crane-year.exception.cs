using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCraneYearException : DomainException
    {
        public InvalidCraneYearException() : base("Invalid crane year.")
        {
        }
    }
}
