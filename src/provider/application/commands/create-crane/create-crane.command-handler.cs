using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.core.Application;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;

namespace ProvidersMicroservice.src.crane.application.commands.create_crane
{
    public class CreateCraneCommandHandler(IIdGenerator<string> idGenerator, IProviderRepository providerRepository) : IApplicationService<CreateCraneCommand, CreateCraneResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<CreateCraneResponse>> Execute(CreateCraneCommand data)
        {
            try
            {
                var providerId = new ProviderId(data.ProviderId);
                var provider = await _providerRepository.GetProviderById(providerId);

                if (!provider.HasValue())
                {
                    return Result<CreateCraneResponse>.Failure(new ProviderNotFoundException());
                }

                var id = _idGenerator.GenerateId();
                var brand = data.Brand;
                var model = data.Model;
                var plate = data.Plate;
                var type = data.Type;
                var year = data.Year;

                var providerWithCrane= provider.Unwrap();
                var crane = providerWithCrane.AddCrane(
                    new CraneId(id),
                    new CraneBrand(brand),
                    new CraneModel(model),
                    new CranePlate(plate),
                    new CraneType(type),
                    new CraneYear(year)
                    );
                
                await _providerRepository.SaveCrane(new SaveCraneDto(providerId, crane));
                return Result<CreateCraneResponse>.Success(new CreateCraneResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateCraneResponse>.Failure(e);
            }
        }
    }
}
