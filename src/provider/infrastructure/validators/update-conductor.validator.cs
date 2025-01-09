using FluentValidation;
using ProvidersMicroservice.src.provider.application.commands.update_conductor.types;

namespace ProvidersMicroservice.src.provider.infrastructure.validators
{
    public class UpdateConductorCommandValidator : AbstractValidator<UpdateConductorCommand>
    {
        public UpdateConductorCommandValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEmpty()
                .WithMessage("Provider id is required");

            RuleFor(x => x.ConductorId)
                .NotEmpty()
                .WithMessage("Conductor id is required");
        }
    }
}
