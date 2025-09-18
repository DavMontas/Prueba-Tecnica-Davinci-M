using core.application.interfaces;
using core.domain.entities;
using infrastructure.persistance.contexts;
using Microsoft.EntityFrameworkCore;

namespace Roulette.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<decimal?> GetBalanceAsync(string name, CancellationToken ct)
    {
        var n = Normalize(name);
        var user = await _db.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.NameNormalized == n, ct);
        return user?.Balance;
    }

    public async Task<decimal> SaveDeltaAsync(string name, decimal delta, CancellationToken ct)
    {
        var n = Normalize(name);

        var updated = await _db.Users
            .Where(x => x.NameNormalized == n)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.Balance, x => x.Balance + delta), ct);

        if (updated == 0)
        {
            _db.Users.Add(new User
            {
                Name = name.Trim(),
                NameNormalized = n,
                Balance = delta
            });

            try
            {
                await _db.SaveChangesAsync(ct);
            }
            catch (DbUpdateException)
            {
                await _db.Users
                    .Where(x => x.NameNormalized == n)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.Balance, x => x.Balance + delta), ct);
            }
        }

        var balance = await _db.Users.AsNoTracking()
            .Where(x => x.NameNormalized == n)
            .Select(x => x.Balance)
            .FirstAsync(ct);

        return balance;
    }

    private static string Normalize(string name) => name.Trim().ToLowerInvariant();
}
