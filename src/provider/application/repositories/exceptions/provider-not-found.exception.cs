using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class ProviderNotFoundException : DomainException
    {
        public ProviderNotFoundException() : base("Provider not found"){ }
    }
}
