using ProvidersMicroservice.src.provider.application.commands.update_conductor.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;

namespace ProvidersMicroservice.src.provider.application.commands.update_conductor
{
    public class UpdateConductorCommandHandler(IProviderRepository providerRepository) : IApplicationService<UpdateConductorCommand, UpdateConductorResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<UpdateConductorResponse>> Execute(UpdateConductorCommand data)
        {
            var providerFound = await _providerRepository.GetProviderById(new ProviderId(data.ProviderId));
            if (!providerFound.HasValue())
            {
                return Result<UpdateConductorResponse>.Failure(new ProviderNotFoundException());
            }
            var provider = providerFound.Unwrap();
            var conductor = provider.GetConductors().Find(c => c.GetId() == data.ConductorId);
            if (conductor == null)
            {
                return Result<UpdateConductorResponse>.Failure(new ConductorNotFoundException());
            }
            if (data.Location != null)
            {
                conductor.ChangeLocation(new ConductorLocation(data.Location));
                await _providerRepository.UpdateConductorLocationById(new ProviderId(data.ProviderId), conductor);
            }
            else 
            {
                conductor.ChangeStatus();
                await _providerRepository.ToggleActivityConductorById(new ProviderId(data.ProviderId), new ConductorId(data.ConductorId));
            }
            return Result<UpdateConductorResponse>.Success(new UpdateConductorResponse(conductor.GetId()));
        }
    }
}
