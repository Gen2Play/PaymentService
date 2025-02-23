﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Payment.API.Persistence;

#nullable disable

namespace Payment.API.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20250223171305_Init_PaymentDb")]
    partial class Init_PaymentDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Payment.API.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("BankAbbreviation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BankSubAccId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CorresponsiveAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CorresponsiveBankId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CorresponsiveBankName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CorresponsiveName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("CusumBalance")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SubAccId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VirtualAccount")
                        .HasColumnType("text");

                    b.Property<string>("VirtualAccountName")
                        .HasColumnType("text");

                    b.Property<DateTime>("When")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
