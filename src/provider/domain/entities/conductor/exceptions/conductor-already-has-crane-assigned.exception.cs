using UsersMicroservice.Core.Domain;
namespace ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions
{
    public class ConductorAlreadyHasCraneAssignedException : DomainException
    {
        public ConductorAlreadyHasCraneAssignedException() : base("Conductor already has a crane assigned")
        {
        }
    }
}