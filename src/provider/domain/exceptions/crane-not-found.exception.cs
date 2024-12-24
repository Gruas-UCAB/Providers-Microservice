using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.exceptions
{
    public class CraneNotFoundException : DomainException
    {
        public CraneNotFoundException() : base("Crane not found")
        {
        }
    }
}
