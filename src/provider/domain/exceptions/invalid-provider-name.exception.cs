using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class InvalidProviderNameException : DomainException
    {
        public InvalidProviderNameException() : base("Invalid provider name.")
        {
        }
    }
}
