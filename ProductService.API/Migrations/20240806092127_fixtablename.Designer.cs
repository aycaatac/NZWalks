﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductService.API.Data;

#nullable disable

namespace ProductService.API.Migrations
{
    [DbContext(typeof(AppProductDbContext))]
    [Migration("20240806092127_fixtablename")]
    partial class fixtablename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductService.API.Models.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "sut urunleri",
                            Description = "productlarda yogurt urunu",
                            Name = "yogurt",
                            Price = 49.990000000000002
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "sut urunleri",
                            Description = "productlarda sut urunu",
                            Name = "sut",
                            Price = 19.989999999999998
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "hamur urunleri",
                            Description = "productlarda ekmek urunu",
                            Name = "ekmek",
                            Price = 1.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "hamur urunleri",
                            Description = "productlarda borek urunu",
                            Name = "borek",
                            Price = 39.990000000000002
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
