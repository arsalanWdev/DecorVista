using DecorVista.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DecorVista.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Smartphones", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Laptops", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Wearables", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Gaming", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Audio", DisplayOrder = 5 }
            );




            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "iPhone 14 Pro",
                    Author = "Apple",
                    Description = "Latest iPhone model with advanced features.",
                    SKU = "IP14PRO",
                    ListPrice = 999.00,
                    Price = 950.00,
                    Price50 = 925.00,
                    Price100 = 900.00,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "MacBook Pro 16-inch",
                    Author = "Apple",
                    Description = "High-performance laptop for professionals.",
                    SKU = "MBP16",
                    ListPrice = 2399.00,
                    Price = 2300.00,
                    Price50 = 2250.00,
                    Price100 = 2200.00,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Apple Watch Series 8",
                    Author = "Apple",
                    Description = "Smartwatch with health tracking features.",
                    SKU = "AW8",
                    ListPrice = 399.00,
                    Price = 375.00,
                    Price50 = 365.00,
                    Price100 = 350.00,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "PlayStation 5",
                    Author = "Sony",
                    Description = "Next-gen gaming console with 4K capabilities.",
                    SKU = "PS5",
                    ListPrice = 499.00,
                    Price = 480.00,
                    Price50 = 470.00,
                    Price100 = 450.00,
                    CategoryId = 4,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Bose QuietComfort 35 II",
                    Author = "Bose",
                    Description = "Noise-cancelling wireless headphones.",
                    SKU = "BOSEQC35II",
                    ListPrice = 299.00,
                    Price = 275.00,
                    Price50 = 265.00,
                    Price100 = 250.00,
                    CategoryId = 5,
                    ImageUrl = ""
                }
            );
        }
    }
}
