﻿using Test.Database.Enums;

namespace Test.Database.Entities
{
    public class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
