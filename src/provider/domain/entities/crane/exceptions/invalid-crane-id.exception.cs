using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCraneIdException : DomainException
    {
        public InvalidCraneIdException() : base("Invalid crane id")
        {
        }
    }
}
