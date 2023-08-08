using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyWeller.LondonStockApi.Domain;

namespace RoyWeller.LondonStockApi.Persistence.Configuration;
public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.ToTable("Trades");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.HasOne(t => t.Broker)
            .WithMany()
            .HasForeignKey(t => t.BrokerId);
    }
}
