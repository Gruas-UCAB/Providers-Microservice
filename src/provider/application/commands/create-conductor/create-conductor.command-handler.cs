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
    public class CreateConductorCommandHandler(IIdGenerator<string> idGenerator, IProviderRepository providerRepository ) : IApplicationService<CreateConductorCommand, CreateConductorResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IProviderRepository _providerRepository = providerRepository;
        public async Task<Result<CreateConductorResponse>> Execute(CreateConductorCommand data)
        {
            try
            {
                var providerId = new ProviderId(data.ProviderId);
                var provider = await _providerRepository.GetProviderById(providerId);

                if (!provider.HasValue())
                {
                    return Result<CreateConductorResponse>.Failure(new ProviderNotFoundException());
                }

                var id = _idGenerator.GenerateId();
                var dni = data.Dni;
                var name = data.Name;
                var image = data.Image;
                var crane = data.CraneId;

                var providerWithConductor = provider.Unwrap();
                var conductor = providerWithConductor.AddConductor(
                        new ConductorId(id), 
                        new ConductorDni(dni), 
                        new ConductorName(name), 
                        new ConductorImage(image), 
                        crane != null ? new CraneId(crane) : null
                    );

                await _providerRepository.SaveConductor(new SaveConductorDto(providerId, conductor));
                return Result<CreateConductorResponse>.Success(new CreateConductorResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateConductorResponse>.Failure(e);
            }
        }
    }
}
