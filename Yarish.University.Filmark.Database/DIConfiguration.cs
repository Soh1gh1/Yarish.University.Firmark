using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yarish.University.Filmark.Database.Interfaces;
using Yarish.University.Filmark.Database.Services;
using Yarish.University.Filmark.Models.Database;
using static Yarish.University.Filmark.Database.Services.ApplicationUser;

namespace Yarish.University.Filmark.Database
{
    public static class DIConfiguration
    {
        public static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<FilmarkDbContext>((x) => x.UseSqlServer(configuration.GetConnectionString("FilmarkDatabase")));

            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<UserService, UserService>();
        }
    }
}
