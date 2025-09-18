using FluentValidation;
using Roulette.Application.Contracts;

namespace Roulette.Api.Validation;

public sealed class SaveBalanceRequestValidator : AbstractValidator<SaveBalanceRequest>
{
    public SaveBalanceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(1)
            .MaximumLength(120);

        RuleFor(x => x.Delta)
            .Must(d => d != 0m).WithMessage("Delta must be non-zero.")
            .Must(d => Math.Round(d, 2) == d).WithMessage("Delta must have at most 2 decimals.");
    }
}
