using Microsoft.EntityFrameworkCore;
using Yarish.University.Filmark.Models.Database.Essense;

public class FilmarkDbContext : DbContext
{
    public DbSet<Phone> Phones { get; set; }

    public FilmarkDbContext() { }

    public FilmarkDbContext(DbContextOptions<FilmarkDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
