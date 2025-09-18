using core.application.dtos;

namespace Roulette.Application.Contracts;

public enum BetType
{
    Color = 1,
    ParityOfColor = 2,
    NumberAndColor = 3
}

public sealed class BetSelection
{
    public int? Number { get; set; }     // 0..36 cuando aplica
    public string? Color { get; set; }   // "red" | "black"
    public string? Parity { get; set; }  // "even" | "odd"
}

public sealed class ResolveBetRequest
{
    public BetType BetType { get; set; }
    public BetSelection Selection { get; set; } = new();
    public decimal Stake { get; set; } // > 0
}

public sealed class ResolveBetResponse
{
    public bool Won { get; set; }
    public decimal Prize { get; set; }      // monto del premio (0 si pierde)
    public SpinResult Outcome { get; set; } = default!;
    public decimal Net { get; set; }        // opcional: Prize - Stake (puede ser negativo)
}

public sealed class SaveBalanceRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Delta { get; set; }      // +/-; != 0
}


public sealed record SpinResult(int Number, string Color, string Parity);
