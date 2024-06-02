using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.Database.Entities.Configurations
{
    public class TestItemConfiguration : IEntityTypeConfiguration<TestItem>
    {
        public void Configure(EntityTypeBuilder<TestItem> builder)
        {
            builder.ToTable("TestItems");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasData(
                new TestItem { Id = 1, Name = "Test 1" },
                new TestItem { Id = 2, Name = "Test 2" },
                new TestItem { Id = 3, Name = "Test 3" },
                new TestItem { Id = 4, Name = "Test 4" },
                new TestItem { Id = 5, Name = "Test 5" }
            );
        }
    }
}
