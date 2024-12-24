using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class InvalidProviderRifException : DomainException
    {
        public InvalidProviderRifException() : base("Invalid provider rif.") { }
    }
}
