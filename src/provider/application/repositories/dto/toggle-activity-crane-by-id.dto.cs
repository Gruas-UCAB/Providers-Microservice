using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;

namespace ProvidersMicroservice.src.provider.application.repositories.dto
{
    public record ToggleActivityCraneByIdDto
    (
        ProviderId providerId,
        CraneId craneId
    );
}
