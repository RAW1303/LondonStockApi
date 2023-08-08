﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoyWeller.LondonStockApi.Persistence;

#nullable disable

namespace RoyWeller.LondonStockApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230807223956_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.20");

            modelBuilder.Entity("RoyWeller.LondonStockApi.Domain.Broker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brokers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alice"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bob"
                        });
                });

            modelBuilder.Entity("RoyWeller.LondonStockApi.Domain.Stock", b =>
                {
                    b.Property<string>("Ticker")
                        .HasColumnType("TEXT");

                    b.HasKey("Ticker");

                    b.ToTable("Stocks", (string)null);
                });

            modelBuilder.Entity("RoyWeller.LondonStockApi.Domain.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrokerId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StockTicker")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("StockTicker");

                    b.ToTable("Trades", (string)null);
                });

            modelBuilder.Entity("RoyWeller.LondonStockApi.Domain.Trade", b =>
                {
                    b.HasOne("RoyWeller.LondonStockApi.Domain.Broker", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoyWeller.LondonStockApi.Domain.Stock", null)
                        .WithMany("Trades")
                        .HasForeignKey("StockTicker");

                    b.Navigation("Broker");
                });

            modelBuilder.Entity("RoyWeller.LondonStockApi.Domain.Stock", b =>
                {
                    b.Navigation("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
