using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;

namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record GetConductorByIdDto
    (
        ProviderId providerId,
        ConductorId conductorId
    );
}
