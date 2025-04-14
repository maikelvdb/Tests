using Test.Database.Enums;

namespace Test.Database.Entities;

public class TestItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public bool? NUllableBool { get; set; }

    public TestEnum Type { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
