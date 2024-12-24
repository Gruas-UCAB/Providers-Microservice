using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class InvalidProviderException : DomainException
    {
        public InvalidProviderException() : base("Invalid provider.")
        {
        }
    }
}
