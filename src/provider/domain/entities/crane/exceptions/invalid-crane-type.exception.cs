using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCraneTypeException : DomainException
    {
        public InvalidCraneTypeException() : base("Invalid crane type.")
        {
        }
    }
}
