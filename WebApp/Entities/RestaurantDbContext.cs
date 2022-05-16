using Microsoft.EntityFrameworkCore;

namespace WebApp.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) 
        { 
            _ = Restaurants ?? throw new InvalidOperationException($"DbSet not Initialized {nameof(Restaurants)}");
            _ = Addresses ?? throw new InvalidOperationException($"DbSet not Initialized {nameof(Addresses)}");
            _ = Dishes ?? throw new InvalidOperationException($"DbSet not Initialized {nameof(Dishes)}");
        }
        private string _connectionString = "Data Source=ACER\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(d => d.Name);
            modelBuilder.Entity<Address>()
                .Property(c => c.City)
                .HasMaxLength(50);
            modelBuilder.Entity<Address>()
                .Property(s => s.Street)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
