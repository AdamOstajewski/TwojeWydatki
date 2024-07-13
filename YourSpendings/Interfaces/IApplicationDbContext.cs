using Microsoft.EntityFrameworkCore;
using YourSpendings.Models;

namespace YourSpendings.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Expense> Expenses { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
