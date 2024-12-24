using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.exceptions
{
    public class InvalidCranePlateException : DomainException
    {
        public InvalidCranePlateException() : base("Invalid crane plate")
        {
        }
    }
}
