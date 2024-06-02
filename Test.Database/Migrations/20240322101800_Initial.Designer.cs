﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.Database;

#nullable disable

namespace Test.Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240322101800_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Test.Database.Entities.TestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("TestItems", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7817),
                            Name = "Test 1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7818),
                            Name = "Test 2"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7819),
                            Name = "Test 3"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7821),
                            Name = "Test 4"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7822),
                            Name = "Test 5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}