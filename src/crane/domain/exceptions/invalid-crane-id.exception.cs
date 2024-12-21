using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneIdException : DomainException
    {
        public InvalidCraneIdException() : base("Invalid crane id")
        {
        }
    }
}
