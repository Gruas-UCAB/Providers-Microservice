using ProvidersMicroservice.src.provider.application.commands.update_crane.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;

namespace ProvidersMicroservice.src.provider.application.commands.update_crane
{
    public class UpdateCraneCommandHandler(IProviderRepository providerRepository) : IApplicationService<UpdateCraneCommand, UpdateCraneResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<UpdateCraneResponse>> Execute(UpdateCraneCommand data)
        {    
            var providerFound = await _providerRepository.GetProviderById(new ProviderId(data.ProviderId));
            if (!providerFound.HasValue())
            {
                return Result<UpdateCraneResponse>.Failure(new ProviderNotFoundException());
            }
            var provider = providerFound.Unwrap();
            var crane = provider.GetCranes().Find(c => c.GetId() == data.CraneId);
            if (crane == null)
            {
                return Result<UpdateCraneResponse>.Failure(new CraneNotFoundException());
            }
            crane.ChangeStatus();
            await _providerRepository.ToggleActivityCraneById(new ProviderId(data.ProviderId), new CraneId(data.CraneId));
            return Result<UpdateCraneResponse>.Success(new UpdateCraneResponse(crane.GetId()));
        }
    }
}
