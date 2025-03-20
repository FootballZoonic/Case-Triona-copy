using Microsoft.EntityFrameworkCore;

namespace LogfileApi.Models
{
    public class LogFileContext : DbContext
    {
        public LogFileContext(DbContextOptions<LogFileContext> options)
            : base(options)
        {
        }

        public DbSet<LogFileItem> LogFileItems { get; set; } = null!;
        public DbSet<LogFileTitle> LogFileTitles { get; set; } = null!;
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .HasMany(a => a.Users)
                .WithOne(u => u.Actor)
                .HasForeignKey(u => u.Actor_ID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.LogFileItems)
                .WithOne()
                .HasForeignKey(l => l.User_ID);
        }
    }
}