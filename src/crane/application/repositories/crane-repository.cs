using ProvidersMicroservice.src.crane.application.repositories.dto;
using ProvidersMicroservice.src.crane.domain;
using ProvidersMicroservice.src.crane.domain.value_objects;
using UsersMicroservice.core.Common;

namespace ProvidersMicroservice.src.crane.application.repositories
{
    public interface ICraneRepository
    {
        public Task<Crane> SaveCrane(Crane crane);
        public Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data);
        public Task<_Optional<Crane>> GetCraneById(CraneId id);
        public Task<CraneId> ToggleActivityCraneById(CraneId id);
    }
}
