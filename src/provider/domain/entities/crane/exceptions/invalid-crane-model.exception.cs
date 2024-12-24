using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCraneModelException : DomainException
    {
        public InvalidCraneModelException() : base("Invalid crane model.")
        {
        }
    }
}
