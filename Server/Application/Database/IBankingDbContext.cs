using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Database
{
    public interface IBankingDbContext 
    {
        DbSet<Transaction> Transactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
