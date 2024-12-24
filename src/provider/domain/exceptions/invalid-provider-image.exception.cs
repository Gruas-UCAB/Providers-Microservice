using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class InvalidProviderImageException : DomainException
    {
        public InvalidProviderImageException() : base("Invalid provider image.") { }
    }
}
