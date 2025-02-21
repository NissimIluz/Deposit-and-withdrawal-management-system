using Application.Database;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ApplicationDBContext : DbContext, IBankingDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}


