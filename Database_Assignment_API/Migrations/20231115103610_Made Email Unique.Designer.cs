﻿// <auto-generated />
using System;
using Database_Assignment_API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database_Assignment_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231115103610_Made Email Unique")]
    partial class MadeEmailUnique
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database_Assignment_API.Entites.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.InStockEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InStock");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.InvoiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("Money");

                    b.Property<decimal>("VAT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.InvoiceLineEntity", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("ProductArticleNumber")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("InvoiceId", "ProductArticleNumber");

                    b.ToTable("InvoiceLines");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("Money");

                    b.Property<decimal>("VAT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.OrderRowEntity", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("ProductArticleNumber")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductArticleNumber");

                    b.HasIndex("ProductArticleNumber");

                    b.ToTable("OrderRows");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.PrimaryCategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrimaryCategories");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.ProductEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal>("StockPrice")
                        .HasColumnType("Money");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ArticleNumber");

                    b.HasIndex("StockId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.SubCategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PrimaryCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.CustomerEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.AddressEntity", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.InvoiceLineEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.InvoiceEntity", "Invoice")
                        .WithMany("InvoiceLines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.OrderEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.CustomerEntity", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.OrderRowEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.OrderEntity", "Order")
                        .WithMany("OrderRows")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database_Assignment_API.Entites.ProductEntity", "Product")
                        .WithMany("OrderRows")
                        .HasForeignKey("ProductArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.ProductEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.InStockEntity", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database_Assignment_API.Entites.SubCategoryEntity", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.SubCategoryEntity", b =>
                {
                    b.HasOne("Database_Assignment_API.Entites.PrimaryCategoryEntity", "PrimaryCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("PrimaryCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrimaryCategory");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.AddressEntity", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.CustomerEntity", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.InvoiceEntity", b =>
                {
                    b.Navigation("InvoiceLines");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.OrderEntity", b =>
                {
                    b.Navigation("OrderRows");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.PrimaryCategoryEntity", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.ProductEntity", b =>
                {
                    b.Navigation("OrderRows");
                });

            modelBuilder.Entity("Database_Assignment_API.Entites.SubCategoryEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
