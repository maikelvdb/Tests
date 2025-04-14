using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.Database.Entities.Configurations;

public class SimpleItemConfiguration : IEntityTypeConfiguration<SimpleItem>
{
    public void Configure(EntityTypeBuilder<SimpleItem> builder)
    {
        builder.ToTable("SimpleItems");
     
        var items = Enumerable.Range(1, 10_001).Select(x => new SimpleItem
        {
            Id = x,
            Name = $"Item {x}",
        });

        builder.HasData(items);
    }
}
