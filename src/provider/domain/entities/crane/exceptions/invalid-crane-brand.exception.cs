using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCraneBrandException : DomainException
    {
        public InvalidCraneBrandException() : base("Invalid crane brand.")
        {
        }
    }
}
