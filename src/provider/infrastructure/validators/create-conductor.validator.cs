using FluentValidation;
using ProvidersMicroservice.src.provider.application.commands.create_conductor.types;

namespace ProvidersMicroservice.src.provider.infrastructure.validators
{
    public class CreateConductorCommandValidator : AbstractValidator<CreateConductorCommand>
    {
        public CreateConductorCommandValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEmpty()
                .WithMessage("Provider id is required");

            RuleFor(x => x.Dni)
                .NotEmpty()
                .WithMessage("Dni is required")
                .GreaterThan(4000000)
                .WithMessage("Dni must be greater than 4.000.000");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters")
                .MaximumLength(25)
                .WithMessage("Name must not exceed 15 characters");

            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("Image is required");
        }
    }
}
