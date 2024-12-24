using FluentValidation;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;

namespace ProvidersMicroservice.src.provider.infrastructure.validators
{
    public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
    {
        public CreateProviderCommandValidator() 
        {
            RuleFor(x => x.Rif)
                .NotEmpty()
                .WithMessage("Rif is required")
                .MinimumLength(10)
                .WithMessage("Rif must be at least 10 characters");

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
