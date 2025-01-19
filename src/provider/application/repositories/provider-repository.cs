using ProvidersMicroservice.src.provider.application.repositories.dto;
using UsersMicroservice.core.Common;
using ProvidersMicroservice.src.provider.domain;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;

namespace ProvidersMicroservice.src.provider.application.repositories
{
    public interface IProviderRepository
    {
        Task<Provider> SaveProvider(Provider provider);
        Task<Crane> SaveCrane(SaveCraneDto data);
        Task<Conductor> SaveConductor(SaveConductorDto data);
        Task<_Optional<List<Provider>>> GetAllProviders(GetAllProvidersDto data);
        Task<_Optional<List<Crane>>> GetAllActiveCranes(GetAllCranesDto data);
        Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data, ProviderId providerId);
        Task<_Optional<Crane>> GetCraneById(ProviderId providerId, CraneId craneId);
        Task<_Optional<Provider>> GetProviderById(ProviderId id);
        Task<_Optional<List<Conductor>>> GetAllActiveConductors(GetAllConductorsDto data);
        Task<_Optional<List<Conductor>>> GetAllConductors(GetAllConductorsDto data, ProviderId providerId);
        Task<_Optional<Conductor>> GetConductorById(ConductorId conductorId);
        Task<ConductorId> UpdateConductorLocationById(Conductor conductor);
        Task<ConductorId> ToggleActivityConductorById(ConductorId craneId);
        Task<CraneId> ToggleActivityCraneById(ProviderId providerId, CraneId craneId);
        Task<ProviderId> ToggleActivityProviderById(ProviderId id);
    }
}
