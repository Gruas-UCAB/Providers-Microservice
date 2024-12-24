using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.value_objects;

namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record SaveConductorDto
    (
        ProviderId providerId,
        Conductor conductor
    );
}
