using UsersMicroservice.Core.Domain;
namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class ConductorNotHaveCraneAssignedException : DomainException
    {
        public ConductorNotHaveCraneAssignedException() : base("Conductor already has a crane assigned")
        {
        }
    }
}