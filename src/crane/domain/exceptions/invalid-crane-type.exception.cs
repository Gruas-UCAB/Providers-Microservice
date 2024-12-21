using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCraneTypeException : DomainException
    {
        public InvalidCraneTypeException() : base("Invalid crane type.")
        {
        }
    }
}
