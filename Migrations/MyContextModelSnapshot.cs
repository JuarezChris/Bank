﻿// <auto-generated />
using System;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Bank.Models.Transactions", b =>
                {
                    b.Property<int>("TransId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("TransId");

                    b.HasIndex("UserId");

                    b.ToTable("trans");
                });

            modelBuilder.Entity("Bank.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Bank.Models.Transactions", b =>
                {
                    b.HasOne("Bank.Models.User", "AccountHolder")
                        .WithMany("Trans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
