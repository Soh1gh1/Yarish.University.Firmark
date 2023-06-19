using Microsoft.EntityFrameworkCore;
using Yarish.University.Filmark.Models.Database;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

public class FilmarkDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    private readonly IConfiguration _configuration;

    public FilmarkDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("FilmarkDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.RegistrationDate)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");

        base.OnModelCreating(modelBuilder);
    }
}