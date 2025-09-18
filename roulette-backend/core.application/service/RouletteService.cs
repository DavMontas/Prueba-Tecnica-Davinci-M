using core.application.dtos;
using core.application.interfaces;
using System.Collections.Immutable;

namespace Roulette.Application.Roulette;

public class RouletteService : IRouletteService
{
    private readonly IRandomProvider _random;

    public RouletteService(IRandomProvider random) => _random = random;

    private static readonly ImmutableHashSet<int> Reds =
        new HashSet<int> { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 }.ToImmutableHashSet();

    public SpinResult Spin()
    {
        var n = _random.NextInt(0, 36);

        // Internamente podemos distinguir "green" para 0
        var color = n == 0 ? "green" : (Reds.Contains(n) ? "red" : "black");
        var parity = n == 0 ? "none" : (n % 2 == 0 ? "even" : "odd");

        // Si quieres que el endpoint sólo reporte rojo/negro, puedes mapear 0 a, por ejemplo, "red" o "black",
        // PERO el enunciado dice “entre rojo y negro”; lo correcto es devolver color real y explicar en entrevista.
        return new SpinResult(n, color, parity);
    }

    public PrizeResult CalculatePrize(Bet bet, SpinResult spin)
    {
        var stake = bet.Stake;
        if (stake <= 0) return new PrizeResult(false, 0m, 0 - Math.Abs(stake));

        bool won = false;
        decimal payout = 0m;

        // Reglas del enunciado:
        // - Color: acierta => gana 0.5 × stake
        // - Par/Impar de un color: acierta => gana 1 × stake
        // - Número + color: acierta => gana 3 × stake
        // - Si no acierta => pierde stake
        // - Si sale 0: color/paridad fallan; number+color sólo gana si decides permitir "0 + (rojo/negro)" (yo lo bloqueo)
        if (spin.Number == 0)
        {
            // Si NO permites apostar "0 + color", entonces todo falla en 0:
            won = false;
            payout = 0m;
        }
        else
        {
            switch (bet.Type)
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
                    if (!string.IsNullOrWhiteSpace(bet.Selection.Color) &&
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
            }
        }

        var net = won ? payout : -stake;
        return new PrizeResult(won, payout, net);
    }
}
