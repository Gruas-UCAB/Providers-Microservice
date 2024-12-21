using Microsoft.AspNetCore.Mvc;
using ProvidersMicroservice.src.crane.application.commands.create_crane;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.repositories;
using ProvidersMicroservice.src.crane.application.repositories.dto;
using ProvidersMicroservice.src.crane.application.repositories.exceptions;
using ProvidersMicroservice.src.crane.infrastructure.repositories;
using ProvidersMicroservice.src.crane.infrastructure.validators;
using UsersMicroservice.core.Application;
using UsersMicroservice.core.Infrastructure;

namespace ProvidersMicroservice.src.crane.infrastructure
{
    [Route("crane")]
    [ApiController]
    public class CraneController : Controller
    {
        private readonly ICraneRepository _craneRepository = new MongoCraneRepository();
        private readonly IIdGenerator<string> _idGenerator = new UUIDGenerator();

        [HttpPost]
        public async Task<IActionResult> CreateCrane(CreateCraneCommand command)
        {
            var validator = new CreateCraneCommandValidator();
            if (!validator.Validate(command).IsValid)
            {
                var errorMessages = validator.Validate(command).Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errorMessages });
            }
            var service = new CreateCraneCommandHandler(_idGenerator, _craneRepository);
            var response = await service.Execute(command);
            if (response.IsFailure)
            {
                return BadRequest(new { errors = response.ErrorMessage() });
            }
            var data = response.Unwrap().Id;
            return Created("Created", new { id = data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCranes([FromQuery] GetAllCranesDto data)
        {
            var cranes = await _craneRepository.GetAllCranes(data);
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
                    Type = c.GetType(),
                    Plate = c.GetPlate(),
                    Year = c.GetYear(),
                }).ToList();
            return Ok(cranesList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCraneById(string id)
        {
            var crane = await _craneRepository.GetCraneById(new domain.value_objects.CraneId(id));
            if (!crane.HasValue())
            {
                return NotFound(new { errorMessage = new CraneNotFoundException().Message });
            }
            var craneData = crane.Unwrap();
            return Ok(new {
                Id = craneData.GetId(),
                Brand = craneData.GetBrand(),
                Model = craneData.GetModel(),
                Type = craneData.GetType(),
                Plate = craneData.GetPlate(),
                Year = craneData.GetYear(),
                IsActive = craneData.IsActive(),
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActivityCraneById(string id)
        {
            try
            {
                var crane = await GetCraneById(id);
                await _craneRepository.ToggleActivityCraneById(new domain.value_objects.CraneId(id));
                return Ok(new { message = "Activity status of crane has been changed" });
            } catch (Exception ex) {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }
    }
}
