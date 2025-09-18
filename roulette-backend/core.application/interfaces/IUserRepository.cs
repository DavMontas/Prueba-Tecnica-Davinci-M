using core.application.dtos;
using core.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByNameAsync(string name, CancellationToken ct = default);
        Task<decimal> GetBalanceAsync(string name, CancellationToken ct = default);
    
        Task<decimal> SaveDeltaAsync(string name, decimal delta, CancellationToken ct = default);
    }
}
