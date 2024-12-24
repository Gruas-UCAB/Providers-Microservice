using ProvidersMicroservice.src.provider.application.repositories.dto;
using UsersMicroservice.core.Common;
using ProvidersMicroservice.src.provider.domain;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane;

namespace ProvidersMicroservice.src.provider.application.repositories
{
    public interface IProviderRepository
    {
        Task<Provider> SaveProvider(Provider provider);
        Task<Crane> SaveCrane(SaveCraneDto data);
        Task<Conductor> SaveConductor(SaveConductorDto data);
        Task<_Optional<List<Provider>>> GetAllProviders(GetAllProvidersDto data);
        Task<_Optional<List<Crane>>> GetAllCranes(GetAllCranesDto data, ProviderId providerId);
        Task<_Optional<Crane>> GetCraneById(GetCraneByIdDto data);
        Task<_Optional<Provider>> GetProviderById(ProviderId id);
        Task<_Optional<List<Conductor>>> GetAllConductors(GetAllConductorsDto data, ProviderId providerId);
        Task<_Optional<Conductor>> GetConductorById(GetConductorByIdDto data);
        Task<ConductorId> ToggleActivityConductorById(ToggleActivityConductorByIdDto data);
        Task<CraneId> ToggleActivityCraneById(ToggleActivityCraneByIdDto data);
        Task<ProviderId> ToggleActivityProviderById(ProviderId id);
        Task<ConductorId> AssignCraneToConductorById(AssignCraneToConductorDto data);
        Task<ConductorId> UnassignCraneFromConductorById(UnassignCraneToConductorDto data);
    }
}
