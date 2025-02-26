using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

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
                
                
                modelBuilder.Entity<Order>()
                    .HasOne<Customer>(order => order.Customer)
                    .WithMany(customer => customer.Orders)
                    .HasForeignKey(order => order.CustomerId);
                
                modelBuilder.Entity<Product>()
                    .HasKey(product => product.Id)
                    .HasName("PK_Product");
                
                modelBuilder.Entity<OrderLine>()
                    .HasKey(orderLine => orderLine.Id)
                    .HasName("PK_OrderLine");
                
                // @Terry: Pretty sure this is not done right. Everywhere seems to think I need to include Order as a column
                // like here https://learn.microsoft.com/en-us/ef/core/modeling/relationships, but not to sure as OrderLine is meant to be the exact replica of db OrderLine
                // See Issue #2 for resolution
                modelBuilder.Entity<OrderLine>()
                    .HasOne<Order>(orderLine => orderLine.Order)
                    .WithMany(order => order.OrderLines)
                    .HasForeignKey(order => order.OrderId);
                
                modelBuilder.Entity<OrderLine>()
                    .HasOne<Product>(orderLine => orderLine.Product)
                    .WithMany(product => product.OrderLines)
                    .HasForeignKey(orderLine => orderLine.ProductId);
                
            base.OnModelCreating(modelBuilder);

        }
}