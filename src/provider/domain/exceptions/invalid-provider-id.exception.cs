using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class InvalidProviderIdException : DomainException
    {
        public InvalidProviderIdException() : base("Invalid provider id.") { }
    }
}
