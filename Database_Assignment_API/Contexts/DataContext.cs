using Database_Assignment_API.Entites;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<InStockEntity> InStock { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderRowEntity> OrderRows { get; set; }
    public DbSet<PrimaryCategoryEntity> PrimaryCategories { get; set; }
    public DbSet<SubCategoryEntity> SubCategories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceLineEntity> InvoiceLines { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
