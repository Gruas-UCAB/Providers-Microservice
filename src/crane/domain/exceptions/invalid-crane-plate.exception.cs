using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain.exceptions
{
    public class InvalidCranePlateException : DomainException
    {
        public InvalidCranePlateException() : base("Invalid crane plate")
        {
        }
    }
}
