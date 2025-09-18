
namespace core.Application.Contracts;

public enum ApiBetType
{
    Color,
    ParityOfColor,
    NumberAndColor
}

public sealed class CalculatePrizeRequest
{
    public ApiBetType BetType { get; set; }
    public decimal Stake { get; set; }
    public BetSelectionDto Selection { get; set; } = new();
    public SpinResultDto Spin { get; set; } = new();
}

public sealed class BetSelectionDto
{
    public string? Color { get; set; } 
    public string? Parity { get; set; }
    public int? Number { get; set; }
}

public sealed class SpinResultDto
{
    public int Number { get; set; }
    public string Color { get; set; } = default!;  
    public string Parity { get; set; } = default!;
}

public sealed class SaveBalanceRequest
{
    public string Name { get; set; } = default!;
    public decimal Delta { get; set; }
}
