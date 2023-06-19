using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yarish.University.Filmark.Database.Interfaces;
using Yarish.University.Filmark.Models.Database;
using Yarish.University.Filmark.Database.Services;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

                // Порівняння хешів паролів
                string enteredHashedPassword = HashPassword(password);
                bool passwordMatches = storedHashedPassword == enteredHashedPassword;

                return passwordMatches;
            }

            public string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                    string hashedPassword = Convert.ToBase64String(hashedBytes);
                    return hashedPassword;
                }
            }

            public async Task<User> GetByEmail(string email)
            {
                return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == email);
            }
        }
    }
}
