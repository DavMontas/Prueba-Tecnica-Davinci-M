using core.application.dtos;
using core.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.mapper
{
    public static class ApiToDomainMapper
    {
        public static Bet ToDomainBet(CalculatePrizeRequest req)
        {
            var betType = req.BetType switch
            {
                ApiBetType.Color => BetType.Color,
                ApiBetType.ParityOfColor => BetType.ParityOfColor,
                ApiBetType.NumberAndColor => BetType.NumberAndColor,
                _ => BetType.Color
            };

            return new Bet(
                Type: betType,
                Stake: req.Stake,
                Selection: new BetSelection(
                    Color: req.Selection.Color,
                    Parity: req.Selection.Parity,
                    Number: req.Selection.Number
                )
            );
        }

        public static SpinResult ToDomainSpin(SpinResultDto dto) =>
            new SpinResult(dto.Number, dto.Color, dto.Parity);
    }
}
