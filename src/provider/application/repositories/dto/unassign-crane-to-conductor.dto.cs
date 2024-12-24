using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;

namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record UnassignCraneToConductorDto
    (
        ProviderId providerId,
        ConductorId conductorId,
        CraneId craneId
    );
}
