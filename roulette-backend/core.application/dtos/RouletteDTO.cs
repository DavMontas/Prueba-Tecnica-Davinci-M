using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.dtos
{
    public enum BetType
    {
        Color,           
        ParityOfColor,   
        NumberAndColor
    }

    public record SpinResult(int Number, string Color, string Parity);

    public record BetSelection(string? Color = null, string? Parity = null, int? Number = null);

    public record Bet(BetType Type, decimal Stake, BetSelection Selection);

    public record PrizeResult(bool Won, decimal Payout, decimal Net);
}
