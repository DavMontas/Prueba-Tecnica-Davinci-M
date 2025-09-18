using core.application.interfaces;
using Roulette.Application.Contracts;

namespace Roulette.Application.Services;

public sealed class RouletteService : IRouletteService
{
    private readonly IRandomProvider _random;
    public RouletteService(IRandomProvider random) => _random = random;

    public SpinResult Spin()
    {
        var n = _random.NextInt(0, 36); // inclusivo
        var color = _random.NextInt(0, 1) == 0 ? "red" : "black"; // independiente
        var parity = (n == 0) ? "zero" : (n % 2 == 0 ? "even" : "odd");
        return new SpinResult(n, color, parity);
    }

    public ResolveBetResponse ResolveBet(ResolveBetRequest req)
    {
        var spin = Spin();
        var (won, payout, net) = CalculatePrize(req, spin);
        return new ResolveBetResponse
        {
            Won = won,
            Prize = payout,   // "monto del premio" (no el neto)
            Outcome = spin,
            Net = net
        };
    }

    private static (bool won, decimal payout, decimal net) CalculatePrize(ResolveBetRequest bet, SpinResult spin)
    {
        var stake = bet.Stake;
        if (stake <= 0) return (false, 0m, -Math.Abs(stake));

        bool won = false;
        decimal payout = 0m;

        switch (bet.BetType)
        {
            case BetType.Color:
                if (!string.IsNullOrWhiteSpace(bet.Selection.Color) &&
                    bet.Selection.Color!.Equals(spin.Color, StringComparison.OrdinalIgnoreCase))
                {
                    won = true;
                    payout = stake * 0.5m;
                }
                break;

            case BetType.ParityOfColor:
                if (spin.Parity != "zero" &&
                    !string.IsNullOrWhiteSpace(bet.Selection.Color) &&
                    !string.IsNullOrWhiteSpace(bet.Selection.Parity) &&
                    bet.Selection.Color!.Equals(spin.Color, StringComparison.OrdinalIgnoreCase) &&
                    bet.Selection.Parity!.Equals(spin.Parity, StringComparison.OrdinalIgnoreCase))
                {
                    won = true;
                    payout = stake * 1.0m;
                }
                break;

            case BetType.NumberAndColor:
                if (bet.Selection.Number is int num &&
                    !string.IsNullOrWhiteSpace(bet.Selection.Color) &&
                    num == spin.Number &&
                    bet.Selection.Color!.Equals(spin.Color, StringComparison.OrdinalIgnoreCase))
                {
                    won = true;
                    payout = stake * 3.0m;
                }
                break;

            default:
                break;
        }

        var net = won ? payout : -stake;
        return (won, payout, net);
    }
}
