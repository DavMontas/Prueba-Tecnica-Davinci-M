using core.application.dtos;
using Roulette.Application.Contracts;

namespace core.application.interfaces;

public interface IRouletteService
{
    SpinResult Spin();
    ResolveBetResponse ResolveBet(ResolveBetRequest req);
}