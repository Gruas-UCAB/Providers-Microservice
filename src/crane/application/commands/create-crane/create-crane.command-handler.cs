using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.repositories;
using ProvidersMicroservice.src.crane.domain;
using ProvidersMicroservice.src.crane.domain.value_objects;
using UsersMicroservice.core.Application;
using UsersMicroservice.Core.Application;
using UsersMicroservice.Core.Common;

namespace ProvidersMicroservice.src.crane.application.commands.create_crane
{
    public class CreateCraneCommandHandler(IIdGenerator<string> idGenerator, ICraneRepository craneRepository) : IApplicationService<CreateCraneCommand, CreateCraneResponse>
    {
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly ICraneRepository _craneRepository = craneRepository;
        public async Task<Result<CreateCraneResponse>> Execute(CreateCraneCommand data)
        {
            try
            {
                var id = _idGenerator.GenerateId();
                var brand = data.Brand;
                var model = data.Model;
                var plate = data.Plate;
                var type = data.Type;
                var year = data.Year;

                var crane = Crane.Create(
                    new CraneId(id),
                    new CraneBrand(brand),
                    new CraneModel(model),
                    new CranePlate(plate),
                    new CraneType(type),
                    new CraneYear(year)
                    );
                var events = crane.PullEvents();
                await _craneRepository.SaveCrane(crane);
                return Result<CreateCraneResponse>.Success(new CreateCraneResponse(id));
            }
            catch (Exception e)
            {
                return Result<CreateCraneResponse>.Failure(e);
            }
        }
    }
}
