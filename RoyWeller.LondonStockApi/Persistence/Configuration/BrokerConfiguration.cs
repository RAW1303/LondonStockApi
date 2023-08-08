using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyWeller.LondonStockApi.Domain;

namespace RoyWeller.LondonStockApi.Persistence.Configuration;
public class BrokerConfiguration : IEntityTypeConfiguration<Broker>
{
    public void Configure(EntityTypeBuilder<Broker> builder)
    {
        builder.ToTable("Brokers");
        builder.HasKey(b => b.Id);

        builder.HasData(new[]
        {
            new Broker { Id = 1, Name = "Alice" },
            new Broker { Id = 2, Name = "Bob" }
        });
    }
}
