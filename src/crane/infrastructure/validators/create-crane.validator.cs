using FluentValidation;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;

namespace ProvidersMicroservice.src.crane.infrastructure.validators
{
    public class CreateCraneCommandValidator : AbstractValidator<CreateCraneCommand>
    {
        public CreateCraneCommandValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Brand is required")
                .MinimumLength(3)
                .WithMessage("Brand must be at least 3 characters")
                .MaximumLength(20)
                .WithMessage("Brand must not exceed 20 characters");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required")
                .MinimumLength(3)
                .WithMessage("Model must be at least 3 characters")
                .MaximumLength(15)
                .WithMessage("Model must not exceed 15 characters");

            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage("Plate is required")
                .Length(7)
                .WithMessage("Plate must be 7 characters");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type is required");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required")
                .InclusiveBetween(2000, 2024)
                .WithMessage("Year must be between 2000 and 2024");
        }
    }
}
