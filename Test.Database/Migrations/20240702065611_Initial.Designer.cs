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
    [Migration("20240702065611_Initial")]
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

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("TestItems", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2851),
                            Name = "Test 1",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2857),
                            Name = "Test 2",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2859),
                            Name = "Test 3",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2861),
                            Name = "Test 4",
                            Type = 0
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2863),
                            Name = "Test 5",
                            Type = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}