using ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;

namespace ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor
{
    public class AssignCraneToConductorCommandHandler(IProviderRepository providerRepository) : IApplicationService<AssignCraneToConductorCommand, AssignCraneToConductorResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<AssignCraneToConductorResponse>> Execute(AssignCraneToConductorCommand data)
        {
            var providerFind = await _providerRepository.GetProviderById(new ProviderId(data.ProviderId)) ?? throw new ProviderNotFoundException();
            var provider = providerFind.Unwrap();
            var conductor = provider.GetConductors().Find(c => c.GetId() == data.ConductorId) ?? throw new ConductorNotFoundException();
            var crane = provider.GetCranes().Find(c => c.GetId() == data.CraneId) ?? throw new CraneNotFoundException();
            provider.AssignCraneToConductor(crane.Id, conductor);
            await _providerRepository.AssignCraneToConductorById(
                new AssignCraneToConductorDto(
                    new ProviderId(data.ProviderId), 
                    new ConductorId(data.ConductorId), 
                    new CraneId(data.CraneId)
                    )
                );
            return Result<AssignCraneToConductorResponse>.Success(new AssignCraneToConductorResponse(conductor.GetId()));
        }
    }
}
