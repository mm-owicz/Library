﻿// <auto-generated />
using Lab2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab2.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Lab2.Models.Book", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("author")
                        .HasColumnType("TEXT");

                    b.Property<int>("date")
                        .HasColumnType("INTEGER");

                    b.Property<string>("leased")
                        .HasColumnType("TEXT");

                    b.Property<string>("publisher")
                        .HasColumnType("TEXT");

                    b.Property<string>("reserved")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .HasColumnType("TEXT");

                    b.Property<string>("user")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Lab2.Models.User", b =>
                {
                    b.Property<string>("user")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("pwd")
                        .HasColumnType("TEXT");

                    b.HasKey("user");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}