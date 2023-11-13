using Database_Assignment_API.Entites;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    DbSet<AddressEntity> Addresses { get; set; }
    DbSet<CustomerEntity> Customers { get; set; }
    DbSet<CustomerInformationEntity> CustomerInformations { get; set; }
    DbSet<CustomerInformationTypeEntity> CustomerInformationTypes { get; set;}
    DbSet<InStockEntity> InStock { get; set; }
    DbSet<OrderEntity> Orders { get; set; }
    DbSet<OrderRowEntity> OrderRows { get; set; }
    DbSet<PrimaryCategoryEntity> PrimaryCategories { get; set; }
    DbSet<SubCategoryEntity> SubCategories { get; set; }
    DbSet<ProductEntity> Products { get; set; }
    DbSet<InvoiceEntity> Invoices { get; set; }
    DbSet<InvoiceLineEntity> InvoiceLines { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerInformationEntity>().HasKey(x => new
        {
            x.CustomerId,
            x.TypeId
        });

        modelBuilder.Entity<OrderRowEntity>().HasKey(x => new
        {
            x.OrderId,
            x.ProductArticleNumber
        });

        modelBuilder.Entity<InvoiceLineEntity>().HasKey(x => new
        {
            x.InvoiceId,
            x.ProductArticleNumber
        });
    }
}
