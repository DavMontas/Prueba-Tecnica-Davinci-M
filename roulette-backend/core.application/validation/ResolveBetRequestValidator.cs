using FluentValidation;
using Roulette.Application.Contracts;

namespace Roulette.Api.Validation;

public sealed class ResolveBetRequestValidator : AbstractValidator<ResolveBetRequest>
{
    public ResolveBetRequestValidator()
    {
        RuleFor(x => x.BetType)
            .IsInEnum();

        RuleFor(x => x.Stake)
            .GreaterThan(0m).WithMessage("Stake must be > 0");

        RuleFor(x => x.Selection)
            .NotNull().WithMessage("Selection is required.");

        When(x => x.BetType == BetType.Color, () =>
        {
            RuleFor(x => x.Selection!.Color)
                .NotEmpty().WithMessage("Color is required.")
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");
        });

        When(x => x.BetType == BetType.ParityOfColor, () =>
        {
            RuleFor(x => x.Selection!.Color)
                .NotEmpty().WithMessage("Color is required.")
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");

            RuleFor(x => x.Selection!.Parity)
                .NotEmpty().WithMessage("Parity is required.")
                .Must(BeValidBetParity).WithMessage("Parity must be 'even' or 'odd'.");
        });

        When(x => x.BetType == BetType.NumberAndColor, () =>
        {
            RuleFor(x => x.Selection!.Number)
                .NotNull().WithMessage("Number is required.")
                .InclusiveBetween(0, 36).WithMessage("Number must be in [0,36].");

            RuleFor(x => x.Selection!.Color)
                .NotEmpty().WithMessage("Color is required.")
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");
        });
    }

    private static bool BeValidBetColor(string? c) =>
        !string.IsNullOrWhiteSpace(c) &&
        (c.Equals("red", StringComparison.OrdinalIgnoreCase) ||
         c.Equals("black", StringComparison.OrdinalIgnoreCase));

    private static bool BeValidBetParity(string? p) =>
        !string.IsNullOrWhiteSpace(p) &&
        (p.Equals("even", StringComparison.OrdinalIgnoreCase) ||
         p.Equals("odd", StringComparison.OrdinalIgnoreCase));
}
