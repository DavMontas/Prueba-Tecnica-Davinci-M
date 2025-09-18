using core.application.interfaces;
using Roulette.Application.Contracts;

namespace Roulette.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) => _repo = repo;

    public Task<decimal?> GetBalance(string name, CancellationToken ct)
        => _repo.GetBalanceAsync(name, ct);

    public Task<decimal> SaveBalance(SaveBalanceRequest req, CancellationToken ct)
        => _repo.SaveDeltaAsync(req.Name, req.Delta, ct);
}
