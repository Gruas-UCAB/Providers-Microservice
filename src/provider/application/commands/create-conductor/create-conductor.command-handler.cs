using ProvidersMicroservice.src.provider.application.commands.create_conductor.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using UsersMicroservice.core.Application;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.application.repositories.dto;

namespace ProvidersMicroservice.src.provider.application.commands.create_conductor
{
    public class CreateConductorCommandHandler(IProviderRepository providerRepository ) : IApplicationService<CreateConductorCommand, CreateConductorResponse>
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<CreateConductorResponse>> Execute(CreateConductorCommand data)
        {
            try
            {
                var provider = await _providerRepository.GetProviderById(new ProviderId(data.ProviderId));
                if (!provider.HasValue())
                {
                    return Result<CreateConductorResponse>.Failure(new ProviderNotFoundException());
                }
                var providerWithConductor = provider.Unwrap();
                var crane = providerWithConductor.GetCranes().Find(c => c.GetId() == data.CraneId);
                if (crane == null)
                {
                    return Result<CreateConductorResponse>.Failure(new CraneNotFoundException());
                }
                var conductor = providerWithConductor.AddConductor(
                        new ConductorId(data.ConductorId), 
                        new ConductorDni(data.Dni), 
                        new ConductorName(data.Name), 
                        new ConductorLocation(data.Location),
                        new ConductorImage(data.Image), 
                        new CraneId(data.CraneId)
                    );
                await _providerRepository.SaveConductor(new SaveConductorDto(new ProviderId(data.ProviderId), conductor));
                return Result<CreateConductorResponse>.Success(new CreateConductorResponse(conductor.GetId()));
            }
            catch (Exception e)
            {
                return Result<CreateConductorResponse>.Failure(e);
            }
        }
    }
}
