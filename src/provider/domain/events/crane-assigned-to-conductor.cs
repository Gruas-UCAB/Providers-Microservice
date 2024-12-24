using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.events
{
    public class CraneAssignedToConductorEvent : DomainEvent<object>
    {
        public CraneAssignedToConductorEvent(string dispatcherId, string name, CraneAssignedToConductor context) : base(dispatcherId, name, context) { }
    }

    public class CraneAssignedToConductor(string CraneId, string ConductorId)
    {
        public string CraneId = CraneId;
        public string ConductorId = ConductorId;

        public static CraneAssignedToConductorEvent CreateEvent(ProviderId DispatcherId, CraneId CraneId, ConductorId ConductorId)
        {
            return new CraneAssignedToConductorEvent(
                DispatcherId.GetId(),
                typeof(CraneAssignedToConductor).Name,
                new CraneAssignedToConductor(
                    CraneId.GetId(),
                    ConductorId.GetId()
                )
            );
        }
    }
}
