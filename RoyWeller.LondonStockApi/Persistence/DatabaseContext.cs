using Microsoft.EntityFrameworkCore;
using RoyWeller.LondonStockApi.Domain;
using System.Reflection;

namespace RoyWeller.LondonStockApi.Persistence;
public class DatabaseContext : DbContext
{
    public virtual DbSet<Broker> Brokers { get; set; }
    public virtual DbSet<Stock> Stocks { get; set; }
    public virtual DbSet<Trade> Trades { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
