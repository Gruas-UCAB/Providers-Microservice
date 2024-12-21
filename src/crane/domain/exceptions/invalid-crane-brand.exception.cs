using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneBrandException : DomainException
    {
        public InvalidCraneBrandException() : base("Invalid crane brand.")
        {
        }
    }
}
