using core.application.dtos;
using core.application.interfaces;
using core.domain.entities;
using infrastructure.persistance.contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.persistance.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) => _db = db;

        private static string Normalize(string name) => name.Trim().ToUpperInvariant();

        public async Task<User?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            var norm = Normalize(name);
            return await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NameNormalized == norm, ct);
        }

        public async Task<decimal> GetBalanceAsync(string name, CancellationToken ct = default)
        {
            var norm = Normalize(name);
            var ub = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NameNormalized == norm, ct);
            return ub?.Balance ?? 0m;
        }
        public async Task<decimal> SaveDeltaAsync(string name, decimal delta, CancellationToken ct = default)
        {
            var norm = Normalize(name);

            var ub = await _db.Users
                .FirstOrDefaultAsync(x => x.NameNormalized == norm, ct);

            if (ub is null)
            {
                ub = new User
                {
                    Name = name.Trim(),
                    NameNormalized = norm,
                    Balance = delta
                };
                _db.Users.Add(ub);
            }
            else
            {
                ub.Balance += delta;
            }

            await _db.SaveChangesAsync(ct);
            return ub.Balance;
        }
    }
}
