using Microsoft.EntityFrameworkCore;
using TerryTraining.Persistence.Models;

namespace TerryTraining.Persistence;

public class TerryDbContext: DbContext
{
    
        public TerryDbContext(DbContextOptions<TerryDbContext> options) : base(options)
        {
            
        }
        
        // DbSet to represent the collection of books in our database
        // public DbSet<Domain.Entities.Product> Product { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }


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