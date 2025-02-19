using Microsoft.EntityFrameworkCore;
using TerryTraining.Models;

namespace TerryTraining;

public class TerryDbContext: DbContext
{
        // DbSet to represent the collection of books in our database
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                modelBuilder.Entity<Customer>()
                    .HasKey(customer => customer.Id)
                    .HasName("PK_Customer");
            
                // Completed from bool to bit
                modelBuilder.Entity<Order>()
                    .Property(order => order.Completed)
                    .HasColumnName("Completed")
                    .HasColumnType("bit");
                
                modelBuilder.Entity<Order>()
                    .HasKey(order => order.Id)
                    .HasName("PK_Order");
                
                // @Terry same here
                modelBuilder.Entity<Order>()
                    .HasMany(orderLine => orderLine.CustomerId)
                    .WithOne(customer => customer.Id)
                    .HasForeignKey("CustomerId");
                
                modelBuilder.Entity<Product>()
                    .HasKey(product => product.Id)
                    .HasName("PK_Product");
                
                modelBuilder.Entity<OrderLine>()
                    .HasKey(orderLine => orderLine.Id)
                    .HasName("PK_OrderLine");
                
                // @Terry: Pretty sure this is not done right. Everywhere seems to think I need to include Order as a column
                // like here https://learn.microsoft.com/en-us/ef/core/modeling/relationships, but not to sure as OrderLine is meant to be the exact replica of db OrderLine
                modelBuilder.Entity<OrderLine>()
                    .HasMany(orderLine => orderLine.OrderId)
                    .WithOne(order => order.Id)
                    .HasForeignKey("OrderId");
                
                // @Terry same here
                modelBuilder.Entity<OrderLine>()
                    .HasMany(orderLine => orderLine.ProductId)
                    .WithOne(product => product.Id)
                    .HasForeignKey("ProductId");
                
            base.OnModelCreating(modelBuilder);

        }
}