using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectPierre.Models;

namespace ProjectPierre.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Aisle> Aisles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aisle>(x => x.HasKey(p => new { p.ProductId, p.CategoryId }));

            modelBuilder.Entity<Aisle>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Aisles)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Aisle>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Aisles)
                .HasForeignKey(c => c.CategoryId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
