using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yarish.University.Filmark.Database.Interfaces;
using Yarish.University.Filmark.Models.Database;
using Yarish.University.Filmark.Database.Services;
using System.Text.RegularExpressions;
using BCrypt.Net;


namespace Yarish.University.Filmark.Database.Services
{
    public class ApplicationUser : User
    {
        public class UserService : DbEntityService<User>
        {
            private readonly FilmarkDbContext _dbContext;

            public UserService(FilmarkDbContext dbContext) : base(dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<bool> UserNameExists(string userName)
            {
                return await _dbContext.Set<User>().AnyAsync(x => x.UserName == userName);
            }

            public async Task<bool> EmailExists(string email)
            {
                return await _dbContext.Set<User>().AnyAsync(x => x.EmailAddress == email);
            }

            public async Task<bool> EmailAndPasswordMatch(string email, string password)
            {
                var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);

                if (user == null)
                {
                    return false;
                }

                string storedHashedPassword = user.Password;

                return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
            }

            public string HashPassword(string userPassword)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);

                return hashedPassword;
            }

            public async Task<User> GetByEmail(string email)
            {
                return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);
            }
        }
    }
}
