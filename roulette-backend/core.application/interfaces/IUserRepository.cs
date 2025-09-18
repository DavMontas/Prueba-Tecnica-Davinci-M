namespace core.application.interfaces;

public interface IUserRepository
{
    Task<decimal?> GetBalanceAsync(string name, CancellationToken ct);
    Task<decimal> SaveDeltaAsync(string name, decimal delta, CancellationToken ct);
}