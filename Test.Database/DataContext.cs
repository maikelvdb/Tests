using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Database
{
    public class DataContext : DbContext
    {
        public static async Task<DataContext> Create()
        {

            var container = new ServiceCollection()
                .AddDbContext<DataContext>();

            var provider = container.BuildServiceProvider();
            var context = provider.GetRequiredService<DataContext>();

            await context.Database.MigrateAsync();



            return context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
