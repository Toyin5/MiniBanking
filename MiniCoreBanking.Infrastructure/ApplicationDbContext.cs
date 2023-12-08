using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniCoreBanking.Domain;

namespace MiniCoreBanking.Infrastructure;
public class ApplicationDbContext : IdentityUserContext<IdentityUser>
{


    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("mini-banking");
        // optionsBuilder.UseSqlite(@"Data Source=./minicorebanking.db;");
    }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();



}
