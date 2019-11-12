﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    [Migration("20191112212805_AddedForeignKeyCompanyToRentalRequestFix")]
    partial class AddedForeignKeyCompanyToRentalRequestFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestCompany.CarRental.Domain.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BonusPointsPerRental")
                        .HasColumnType("int");

                    b.Property<int>("Brand")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerDay")
                        .HasColumnType("int");

                    b.Property<int>("PriceType")
                        .HasColumnType("int");

                    b.Property<string>("Registration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Rented")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RentedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RentedUntilDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("TestCompany.CarRental.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BonusPoints")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TestCompany.CarRental.Domain.Requests.RentRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StatusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("RentRequest");
                });

            modelBuilder.Entity("TestCompany.CarRental.Domain.Entities.Car", b =>
                {
                    b.HasOne("TestCompany.CarRental.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("TestCompany.CarRental.Domain.Requests.RentRequest", b =>
                {
                    b.HasOne("TestCompany.CarRental.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });
#pragma warning restore 612, 618
        }
    }
}
