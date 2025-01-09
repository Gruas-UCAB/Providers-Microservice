using FluentValidation;
using ProvidersMicroservice.src.provider.application.commands.update_crane.types;

namespace ProvidersMicroservice.src.provider.infrastructure.validators
{
    public class UpdateCraneCommandValidator : AbstractValidator<UpdateCraneCommand>
    {
        public UpdateCraneCommandValidator() 
        {
            RuleFor(x => x.ProviderId)
                .NotEmpty()
                .WithMessage("Provider id is required");

            RuleFor(x => x.CraneId)
                .NotEmpty()
                .WithMessage("Crane id is required");
        }
    }
}
