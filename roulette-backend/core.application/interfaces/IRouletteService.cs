using core.application.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.interfaces
{
    public interface IRouletteService
    {
        SpinResult Spin();
        PrizeResult CalculatePrize(Bet bet, SpinResult spin);
    }
}
