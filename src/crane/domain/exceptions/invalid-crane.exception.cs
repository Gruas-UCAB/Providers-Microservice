using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneException : DomainException
    {
        public InvalidCraneException() : base("Invalid crane.")
        {
        }
    }
}
