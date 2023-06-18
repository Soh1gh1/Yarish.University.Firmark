using Microsoft.EntityFrameworkCore;
using Yarish.University.Filmark.Models.Database;

public class FilmarkDbContext : DbContext
{
    public DbSet<User> users { get; set; }

    public FilmarkDbContext() { }

    public FilmarkDbContext(DbContextOptions<FilmarkDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies();
        options.UseSqlServer("");
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
