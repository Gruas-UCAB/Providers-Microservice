using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProvidersMicroservice.src.crane.application.commands.create_crane;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.commands.create_provider;
using ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor;
using ProvidersMicroservice.src.provider.application.commands.assign_crane_to_conductor.types;
using ProvidersMicroservice.src.provider.application.commands.create_conductor;
using ProvidersMicroservice.src.provider.application.commands.create_conductor.types;
using ProvidersMicroservice.src.provider.application.commands.unassign_crane_to_conductor.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.infrastructure.repositories;
using ProvidersMicroservice.src.provider.infrastructure.validators;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;
using UsersMicroservice.core.Application;
using UsersMicroservice.core.Infrastructure;

namespace ProvidersMicroservice.src.provider.infrastructure
{
    [Route("provider")]
    [ApiController]
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _providerRepository = new MongoProviderRepository();
        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateProvider(CreateProviderCommand command)
        {
            var validator = new CreateProviderCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateProviderCommandHandler(_idGenerator, _providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpPost("conductors")]
        public async Task<IActionResult> CreateConductor(CreateConductorCommand command)
        {
            var validator = new CreateConductorCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateConductorCommandHandler(_idGenerator, _providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpPost("cranes")]
        public async Task<IActionResult> CreateCrane(CreateCraneCommand command)
        {
            var validator = new CreateCraneCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateCraneCommandHandler(_idGenerator, _providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders([FromQuery] GetAllProvidersDto data)
        {
            var providers = await _providerRepository.GetAllProviders(data);
            if (!providers.HasValue())
            {
                return NotFound(new { errorMessage = new NoProvidersFoundException().Message });
            }
            var providersList = providers.Unwrap().Select(
                p => new
                {
                    Id = p.GetId(),
                    Rif = p.GetRif(),
                    Name = p.GetName(),
                    Image = p.GetImage(),
                    Conductors = p.GetConductors().Select(conductor => new
                    {
                        Id = conductor.GetId(),
                        Dni = conductor.GetDni(),
                        Name = conductor.GetName(),
                        Image = conductor.GetImage(),
                        AssignedCrane = conductor.GetAssignedCrane(),
                        IsActive = conductor.IsActive()
                    }).ToList(),
                    Cranes = p.GetCranes().Select(crane => new
                    {
                        Id = crane.GetId(),
                        Brand = crane.GetBrand(),
                        Model = crane.GetModel(),
                        Plate = crane.GetPlate(),
                        Type = crane.GetType(),
                        Year = crane.GetYear(),
                        IsActive = crane.IsActive()
                    }).ToList()
                }).ToList();
            return Ok(providersList);
        }

        [HttpGet("conductors/{id}")]
        public async Task<IActionResult> GetAllConductors([FromQuery] GetAllConductorsDto data, string id)
        {
            var conductors = await _providerRepository.GetAllConductors(data, new ProviderId(id));
            if (!conductors.HasValue())
            {
                return NotFound(new { errorMessage = new NoConductorsFoundException().Message });
            }
            var conductorsList = conductors.Unwrap().Select(
                c => new
                {
                    Id = c.GetId(),
                    Dni = c.GetDni(),
                    Name = c.GetName(),
                    Image = c.GetImage(),
                    AssignedCrane = c.GetAssignedCrane(),
                    IsActive = c.IsActive()
                }).ToList();
            return Ok(conductorsList);
        }

        [HttpGet("cranes/{id}")]
        public async Task<IActionResult> GetAllCranes([FromQuery] GetAllCranesDto data, string id)
        {
            var cranes = await _providerRepository.GetAllCranes(data, new ProviderId(id));
            if (!cranes.HasValue())
            {
                return NotFound(new { errorMessage = new NoCranesFoundException().Message });
            }
            var cranesList = cranes.Unwrap().Select(
                c => new
                {
                    Id = c.GetId(),
                    Brand = c.GetBrand(),
                    Model = c.GetModel(),
                    Plate = c.GetPlate(),
                    Type = c.GetType(),
                    Year = c.GetYear(),
                    IsActive = c.IsActive()
                }).ToList();
            return Ok(cranesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(string id)
        {
            var providerId = new ProviderId(id);
            var provider = await _providerRepository.GetProviderById(providerId);
            if (!provider.HasValue())
            {
                return NotFound(new { errorMessage = new ProviderNotFoundException().Message });
            }
            var data = provider.Unwrap();
            return Ok(new
            {
                Id = data.GetId(),
                Rif = data.GetRif(),
                Name = data.GetName(),
                Image = data.GetImage(),
                Conductors = data.GetConductors().Select(conductor => new
                {
                    Id = conductor.GetId(),
                    Dni = conductor.GetDni(),
                    Name = conductor.GetName(),
                    Image = conductor.GetImage(),
                    AssignedCrane = conductor.GetAssignedCrane(),
                    IsActive = conductor.IsActive()
                }).ToList(),
                Cranes = data.GetCranes().Select(crane => new
                {
                    Id = crane.GetId(),
                    Brand = crane.GetBrand(),
                    Model = crane.GetModel(),
                    Plate = crane.GetPlate(),
                    Type = crane.GetType(),
                    Year = crane.GetYear(),
                    IsActive = crane.IsActive()
                }).ToList()
            });
        }

        [HttpGet("conductors/conductor/{conductorId}")]
        public async Task<IActionResult> GetConductorById([FromQuery]string providerId, string conductorId)
        {
            try
            {
                var conductor = await _providerRepository.GetConductorById(new GetConductorByIdDto(new ProviderId(providerId), new ConductorId(conductorId)));
                if (!conductor.HasValue())
                {
                    return NotFound(new { errorMessage = new ConductorNotFoundException().Message });
                }
                var data = conductor.Unwrap();
                return Ok(new
                {
                    Id = data.GetId(),
                    Dni = data.GetDni(),
                    Name = data.GetName(),
                    Image = data.GetImage(),
                    Crane = data.GetAssignedCrane()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message});
            }
        }

        [HttpGet("cranes/crane")]
        public async Task<IActionResult> GetCraneById([FromQuery] string providerId, [FromBody]string craneId)
        {
            try
            {
                var crane = await _providerRepository.GetCraneById(new GetCraneByIdDto(providerId, craneId));
                if (!crane.HasValue())
                {
                    return NotFound(new { errorMessage = new CraneNotFoundException().Message });
                }
                var data = crane.Unwrap();
                return Ok(new
                {
                    Id = data.GetId(),
                    Brand = data.GetBrand(),
                    Model = data.GetModel(),
                    Plate = data.GetPlate(),
                    Type = data.GetType(),
                    Year = data.GetYear()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }

        [HttpPut("conductors/assign")]
        public async Task<IActionResult> AssignCraneToConductor(AssignCraneToConductorCommand command)
        {
            var service = new AssignCraneToConductorCommandHandler(_providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Ok(new { id = data });
        }

        [HttpPut("conductors/unassign/{id}")]
        public async Task<IActionResult> UnassignCraneToConductor(UnassignCraneToConductorCommand command)
        {
            var service = new UnassignCraneToConductorCommandHandler(_providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Ok(new { id = data });
        }
    }
}
