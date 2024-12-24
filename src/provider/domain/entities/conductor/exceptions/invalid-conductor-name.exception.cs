using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class InvalidConductorNameException : DomainException
    {
        public InvalidConductorNameException() : base("Invalid conductor name.")
        { }
    }
}
