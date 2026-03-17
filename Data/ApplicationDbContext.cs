using Microsoft.EntityFrameworkCore;
using FurnitureOrderingSystemAdmin.Models;

namespace FurnitureOrderingSystemAdmin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FurnitureOrderingSystemAdmin.Models.Combo> Combos { get; set; }
        public DbSet<FurnitureOrderingSystemAdmin.Models.ComboItem> ComboItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CustomerCoupon> CustomerCoupons { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision
            modelBuilder.Entity<Furniture>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Configure relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Furnitures)
                .WithOne(f => f.Category)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Furniture>()
                .HasMany(f => f.OrderItems)
                .WithOne(oi => oi.Furniture)
                .HasForeignKey(oi => oi.FurnitureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Furniture>()
                .HasMany(f => f.CartItems)
                .WithOne(ci => ci.Furniture)
                .HasForeignKey(ci => ci.FurnitureId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin 
                { 
                    Id = 1, 
                    FirstName = "Super", 
                    LastName = "Admin", 
                    Email = "admin@furniturehub.com", 
                    Phone = "9876543210",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"), 
                    Role = "SuperAdmin",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sofas", Description = "Comfortable sofas and couches", ImageUrl = "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=400" },
                new Category { Id = 2, Name = "Chairs", Description = "Dining and office chairs", ImageUrl = "https://images.unsplash.com/photo-1506439773649-6e0eb8cfb237?w=400" },
                new Category { Id = 3, Name = "Tables", Description = "Dining and coffee tables", ImageUrl = "https://images.unsplash.com/photo-1449247709967-d4461a6a6103?w=400" },
                new Category { Id = 4, Name = "Beds", Description = "Comfortable beds and mattresses", ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=400" },
                new Category { Id = 5, Name = "Storage", Description = "Wardrobes and storage solutions", ImageUrl = "https://images.unsplash.com/photo-1558618047-3c8c76ca7d13?w=400" }
            );

            // Seed Furniture
            modelBuilder.Entity<Furniture>().HasData(
                // Sofas
                new Furniture { Id = 1, Name = "Modern 3-Seater Sofa", Description = "Comfortable modern sofa with premium fabric", Price = 45000, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=600", Color = "Gray", Material = "Fabric", Dimensions = "200x90x85 cm", Stock = 15 },
                new Furniture { Id = 2, Name = "Leather Recliner Sofa", Description = "Luxurious leather recliner sofa", Price = 65000, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=600", Color = "Brown", Material = "Leather", Dimensions = "180x95x90 cm", Stock = 8 },
                
                // Chairs
                new Furniture { Id = 3, Name = "Ergonomic Office Chair", Description = "Comfortable office chair with lumbar support", Price = 12000, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1506439773649-6e0eb8cfb237?w=600", Color = "Black", Material = "Mesh", Dimensions = "60x60x120 cm", Stock = 25 },
                new Furniture { Id = 4, Name = "Wooden Dining Chair", Description = "Classic wooden dining chair", Price = 3500, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1549497538-303791108f95?w=600", Color = "Natural", Material = "Wood", Dimensions = "45x45x85 cm", Stock = 40 },
                
                // Tables
                new Furniture { Id = 5, Name = "Glass Coffee Table", Description = "Modern glass coffee table with metal legs", Price = 18000, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1449247709967-d4461a6a6103?w=600", Color = "Clear", Material = "Glass", Dimensions = "120x60x45 cm", Stock = 12 },
                new Furniture { Id = 6, Name = "Wooden Dining Table", Description = "Solid wood dining table for 6 people", Price = 35000, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1581539250439-c96689b516dd?w=600", Color = "Natural", Material = "Wood", Dimensions = "180x90x75 cm", Stock = 6 },
                
                // Beds
                new Furniture { Id = 7, Name = "King Size Bed", Description = "Comfortable king size bed with headboard", Price = 55000, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=600", Color = "White", Material = "Wood", Dimensions = "200x180x120 cm", Stock = 10 },
                new Furniture { Id = 8, Name = "Single Bed", Description = "Simple single bed for kids room", Price = 15000, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1540932239986-30128078f3c5?w=600", Color = "Blue", Material = "Wood", Dimensions = "100x200x90 cm", Stock = 20 },
                
                // Storage
                new Furniture { Id = 9, Name = "3-Door Wardrobe", Description = "Spacious wardrobe with mirror", Price = 28000, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1558618047-3c8c76ca7d13?w=600", Color = "White", Material = "Wood", Dimensions = "150x60x200 cm", Stock = 8 },
                new Furniture { Id = 10, Name = "Bookshelf", Description = "5-tier wooden bookshelf", Price = 8500, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=600", Color = "Natural", Material = "Wood", Dimensions = "80x30x180 cm", Stock = 15 }
            );
        }
    }
}
