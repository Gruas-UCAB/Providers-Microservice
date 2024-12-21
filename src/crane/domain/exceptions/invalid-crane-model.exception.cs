using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneModelException : DomainException
    {
        public InvalidCraneModelException() : base("Invalid crane model.")
        {
        }
    }
}
