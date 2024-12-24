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
    public class CreateProviderCommandHandler(IIdGenerator<string> idGenerator, IProviderRepository providerRepository) : IApplicationService<CreateProviderCommand, CreateProviderResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IProviderRepository _providerRepository = providerRepository;

        public async Task<Result<CreateProviderResponse>> Execute(CreateProviderCommand data)
        {
            try
            {
                var id = _idGenerator.GenerateId();
                var rif = data.Rif;
                var name = data.Name;
                var image = data.Image;

                var provider = Provider.Create(
                    new ProviderId(id),
                    new ProviderName(name),
                    new ProviderRif(rif),
                    new ProviderImage(image),
                    new List<Conductor>(),
                    new List<Crane>()
                    );

                await _providerRepository.SaveProvider(provider);
                return Result<CreateProviderResponse>.Success(new CreateProviderResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateProviderResponse>.Failure(e);
            }
        }
    }
}
