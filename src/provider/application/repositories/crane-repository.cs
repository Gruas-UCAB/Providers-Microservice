using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using UsersMicroservice.core.Common;

namespace ProvidersMicroservice.src.provider.application.repositories
{
    public interface ICraneRepository
    {
        Task<Crane> SaveCrane(Crane crane);
        Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data);
        Task<_Optional<Crane>> GetCraneById(CraneId id);
        Task<CraneId> ToggleActivityCraneById(CraneId id);
    }
}
