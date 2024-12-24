using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.application.commands.unassign_crane_to_conductor.types;

namespace ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor
{
    public class UnassignCraneToConductorCommandHandler(IProviderRepository providerRepository) : IApplicationService<UnassignCraneToConductorCommand, UnassignCraneToConductorResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<UnassignCraneToConductorResponse>> Execute(UnassignCraneToConductorCommand data)
        {
            var providerFind = await _providerRepository.GetProviderById(new ProviderId(data.ProviderId)) ?? throw new ProviderNotFoundException();
            var provider = providerFind.Unwrap();
            var conductor = provider.GetConductors().Find(c => c.Id == new ConductorId(data.ConductorId)) ?? throw new ConductorNotFoundException();
            provider.RemoveCraneFromConductor(conductor, new CraneId(data.CraneId));
            return Result<UnassignCraneToConductorResponse>.Success(new UnassignCraneToConductorResponse(conductor.GetId()));
        }
    }
}
