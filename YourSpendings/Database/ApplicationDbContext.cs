using Microsoft.EntityFrameworkCore;
using YourSpendings.Interfaces;
using YourSpendings.Models;

namespace YourSpendings.Database
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(512).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Expenses)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FirstName).HasMaxLength(128).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(128).IsRequired();

                entity.HasMany(u => u.Expenses)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
