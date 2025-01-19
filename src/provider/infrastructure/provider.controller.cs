using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvidersMicroservice.src.crane.application.commands.create_crane;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.commands.create_provider;
using ProvidersMicroservice.src.provider.application.commands.create_conductor;
using ProvidersMicroservice.src.provider.application.commands.create_conductor.types;
using ProvidersMicroservice.src.provider.application.commands.update_conductor;
using ProvidersMicroservice.src.provider.application.commands.update_conductor.types;
using ProvidersMicroservice.src.provider.application.commands.update_crane;
using ProvidersMicroservice.src.provider.application.commands.update_crane.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.application.repositories.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.value_objects;
using ProvidersMicroservice.src.provider.infrastructure.dto;
using ProvidersMicroservice.src.provider.infrastructure.repositories;
using ProvidersMicroservice.src.provider.infrastructure.validators;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;
using RestSharp;
using UsersMicroservice.core.Application;

namespace ProvidersMicroservice.src.provider.infrastructure
{
    [Authorize]
    [Route("provider")]
    [ApiController]
    public class ProviderController(IProviderRepository providerRepository, IIdGenerator<string> idGenerator, IRestClient restClient) : Controller
    {
        private readonly IProviderRepository _providerRepository = providerRepository;
        private readonly IIdGenerator<string> _idGenerator = idGenerator;
        private readonly IRestClient _restClient = restClient;

        [Authorize(Policy = "AdminUser")]
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody]CreateProviderCommand command, [FromHeader(Name = "Authorization")] string token)
        {
            var userExistsRequest = new RestRequest($"https://localhost:5350/user/{command.Id}", Method.Get);
            userExistsRequest.AddHeader("Authorization", token);
            var userFind = await _restClient.ExecuteAsync(userExistsRequest);
            if (!userFind.IsSuccessful)
            {
                return NotFound(new { errorMessage = userFind.Content });
            }
            if (!userFind.Content.Contains("provider"))
            {
                return Unauthorized(new { errorMessage = "Provider Id not correspond to a provider user" });
            }
            var validator = new CreateProviderCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateProviderCommandHandler(_providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [Authorize(Policy = "CreationalUser")]
        [HttpPost("conductors")]
        public async Task<IActionResult> CreateConductor(CreateConductorCommand command, [FromHeader(Name = "Authorization")] string token)
        {
            var userExistsRequest = new RestRequest($"https://localhost:5350/user/{command.ConductorId}", Method.Get);
            userExistsRequest.AddHeader("Authorization", token);
            var userFind = await _restClient.ExecuteAsync(userExistsRequest);
            if (!userFind.IsSuccessful)
            {
                return NotFound(new { errorMessage = userFind.Content });
            }
            if (!userFind.Content.Contains("conductor"))
            {
                return Unauthorized(new { errorMessage = "The conductor id not correspond to a conductor user" });
            }
            var validator = new CreateConductorCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateConductorCommandHandler(_providerRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [Authorize(Policy = "CreationalUser")]
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
                        Location = conductor.GetLocation(),
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

        [HttpGet("conductors")]
        public async Task<IActionResult> GetAllActiveConductors([FromQuery] GetAllConductorsDto data)
        {
            var conductors = await _providerRepository.GetAllActiveConductors(data);
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
                    Location = c.GetLocation(),
                    Image = c.GetImage(),
                    AssignedCrane = c.GetAssignedCrane(),
                    IsActive = c.IsActive()
                }).ToList();
            return Ok(conductorsList);
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
                    Location = c.GetLocation(),
                    Image = c.GetImage(),
                    AssignedCrane = c.GetAssignedCrane(),
                    IsActive = c.IsActive()
                }).ToList();
            return Ok(conductorsList);
        }

        [HttpGet("cranes")]
        public async Task<IActionResult> GetAllActiveCranes([FromQuery] GetAllCranesDto data)
        {
            var cranes = await _providerRepository.GetAllActiveCranes(data);
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
                    Location = conductor.GetLocation(),
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
        public async Task<IActionResult> GetConductorById(string conductorId)
        {
            try
            {
                var conductor = await _providerRepository.GetConductorById(new ConductorId(conductorId));
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
                    Location = data.GetLocation(),
                    Image = data.GetImage(),
                    Crane = data.GetAssignedCrane()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message });
            }
        }

        [HttpGet("cranes/crane/{providerId}")]
        public async Task<IActionResult> GetCraneById([FromBody] GetCraneByIdDto query, string providerId)
        {
            try
            {
                var crane = await _providerRepository.GetCraneById(new ProviderId(providerId), new CraneId(query.CraneId));
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

        [HttpPatch("conductors/conductor/location/{conductorId}")]
        public async Task<IActionResult> UpdateConductorLocation([FromBody] UpdateConductorDto data, string conductorId)
        {
            try
            {
                var command = new UpdateConductorCommand(conductorId, data.Location);
                if (string.IsNullOrEmpty(command.Location))
                {
                    return BadRequest(new { errorMessage = "Location is required" });
                }
                var validator = new UpdateConductorCommandValidator();
                if (!validator.Validate(command).IsValid)
                {
                    var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                var service = new UpdateConductorCommandHandler(_providerRepository);
                var response = await service.Execute(command);
                if (response.IsFailure)
                {
                    return BadRequest(new { errors = response.ErrorMessage() });
                }
                return Ok(new { message = "Conductor location updated successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errors = e.Message });
            }
        }

        [HttpPatch("conductors/conductor/toggle-activity/{conductorId}")]
        public async Task<IActionResult> ToggleConductorActivity([FromBody] UpdateConductorDto data, string conductorId)
        {
            try
            {
                var command = new UpdateConductorCommand(conductorId, data.Location);
                if (!string.IsNullOrEmpty(command.Location))
                {
                    return BadRequest(new { errorMessage = "Location is not required" });
                }
                var validator = new UpdateConductorCommandValidator();
                if (!validator.Validate(command).IsValid)
                {
                    var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                var service = new UpdateConductorCommandHandler(_providerRepository);
                var response = await service.Execute(command);
                if (response.IsFailure)
                {
                    return BadRequest(new { errors = response.ErrorMessage() });
                }
                return Ok(new { message = "Conductor activity toggled successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errors = e.Message });
            }
        }

        [HttpPatch("cranes/crane/toggle-activity/{providerId}")]
        public async Task<IActionResult> ToggleCraneActivity([FromBody] UpdateCraneDto data, string providerId)
        {
            try
            {
                var command = new UpdateCraneCommand(providerId, data.CraneId);
                var validator = new UpdateCraneCommandValidator();
                if (!validator.Validate(command).IsValid)
                {
                    var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = errorMessages });
                }
                var service = new UpdateCraneCommandHandler(_providerRepository);
                var response = await service.Execute(command);
                if (response.IsFailure)
                {
                    return BadRequest(new { errors = response.ErrorMessage() });
                }
                return Ok(new { message = "Crane activity toggled successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { errors = e.Message });
            }
        }
    }
}
