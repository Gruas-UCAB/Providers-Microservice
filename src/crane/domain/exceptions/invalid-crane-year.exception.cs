using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneYearException : DomainException
    {
        public InvalidCraneYearException() : base("Invalid crane year.")
        {
        }
    }
}
