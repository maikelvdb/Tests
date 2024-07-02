using Microsoft.EntityFrameworkCore;
using Test.Database;
using Test.Database.Entities;

using var db = await DataContext.Create()
    ?? throw new InvalidOperationException("Could not get DataContext from service provider");

await db.Database.MigrateAsync();

db.Set<TestItem>().Add(new TestItem()
{
    Name = "Test Item",
    Type = Test.Database.Enums.TestEnum.Default,
});

await db.SaveChangesAsync();

var all = await db.Set<TestItem>().ToListAsync();

foreach (var item in all)
{
    Console.WriteLine($"Item: {item.Name} - {item.Type}");
}
