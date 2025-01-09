using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.commands.create_provider.types;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.domain;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;
using UsersMicroservice.core.Application;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.crane;

namespace ProvidersMicroservice.src.crane.application.commands.create_provider
{
    public class CreateProviderCommandHandler(IProviderRepository providerRepository) : IApplicationService<CreateProviderCommand, CreateProviderResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;

        public async Task<Result<CreateProviderResponse>> Execute(CreateProviderCommand data)
        {
            try
            {
                var provider = Provider.Create(
                    new ProviderId(data.Id),
                    new ProviderName(data.Name),
                    new ProviderRif(data.Rif),
                    new ProviderImage(data.Image),
                    new List<Conductor>(),
                    new List<Crane>()
                    );

                await _providerRepository.SaveProvider(provider);
                return Result<CreateProviderResponse>.Success(new CreateProviderResponse(provider.GetId()));
            }
            catch (Exception e)
            {
                return Result<CreateProviderResponse>.Failure(e);
            }
        }
    }
}
