using core.Application.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CalculatePrizeRequestValidator : AbstractValidator<CalculatePrizeRequest>
{
    public CalculatePrizeRequestValidator()
    {
        RuleFor(x => x.BetType).IsInEnum();

        RuleFor(x => x.Stake)
            .GreaterThan(0).WithMessage("Stake must be > 0");

        RuleFor(x => x.Selection)
            .NotNull();

        RuleFor(x => x.Spin)
            .NotNull();

        // Spin validations
        When(x => x.Spin is not null, () =>
        {
            RuleFor(x => x.Spin!.Number)
                .InclusiveBetween(0, 36);

            RuleFor(x => x.Spin!.Color)
                .NotEmpty()
                .Must(BeValidSpinColor).WithMessage("Spin color must be red/black/green.");

            RuleFor(x => x.Spin!.Parity)
                .NotEmpty()
                .Must(BeValidSpinParity).WithMessage("Spin parity must be even/odd/none.");
        });

        // Selection validations per BetType
        When(x => x.BetType == ApiBetType.Color, () =>
        {
            RuleFor(x => x.Selection!.Color)
                .NotEmpty()
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");
        });

        When(x => x.BetType == ApiBetType.ParityOfColor, () =>
        {
            RuleFor(x => x.Selection!.Color)
                .NotEmpty()
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");

            RuleFor(x => x.Selection!.Parity)
                .NotEmpty()
                .Must(BeValidBetParity).WithMessage("Parity must be 'even' or 'odd'.");
        });

        When(x => x.BetType == ApiBetType.NumberAndColor, () =>
        {
            RuleFor(x => x.Selection!.Number)
                .NotNull()
                .InclusiveBetween(0, 36);

            RuleFor(x => x.Selection!.Color)
                .NotEmpty()
                .Must(BeValidBetColor).WithMessage("Color must be 'red' or 'black'.");
        });
    }

    private static bool BeValidBetColor(string? c) =>
        c is not null && (c.Equals("red", StringComparison.OrdinalIgnoreCase) ||
                          c.Equals("black", StringComparison.OrdinalIgnoreCase));

    private static bool BeValidBetParity(string? p) =>
        p is not null && (p.Equals("even", StringComparison.OrdinalIgnoreCase) ||
                          p.Equals("odd", StringComparison.OrdinalIgnoreCase));

    private static bool BeValidSpinColor(string? c) =>
        c is not null && (BeValidBetColor(c) ||
                          c.Equals("green", StringComparison.OrdinalIgnoreCase));

    private static bool BeValidSpinParity(string? p) =>
        p is not null && (BeValidBetParity(p) ||
                          p.Equals("none", StringComparison.OrdinalIgnoreCase));
}