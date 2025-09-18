
using Roulette.Application.Contracts;

namespace core.application.interfaces;


public interface IUserService
{
    Task<decimal?> GetBalance(string name, CancellationToken ct);
    Task<decimal> SaveBalance(SaveBalanceRequest req, CancellationToken ct);
}