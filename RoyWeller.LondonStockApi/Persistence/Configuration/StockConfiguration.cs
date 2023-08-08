using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyWeller.LondonStockApi.Domain;

namespace RoyWeller.LondonStockApi.Persistence.Configuration;
public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");
        builder.HasKey(s => s.Ticker);

        builder.HasMany(s => s.Trades).WithOne().HasForeignKey(t => t.StockTicker);
    }
}
