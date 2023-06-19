using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Yarish.University.Filmark.Database.Interfaces;
using Yarish.University.Filmark.Models.Database;
using Yarish.University.Filmark.Database.Services;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Yarish.University.Filmark.Database;
using Yarish.University.Filmark.Models.Frontend;
using System.Security.Cryptography;
using System.Text;
using static Yarish.University.Filmark.Database.Services.ApplicationUser;

namespace Yarish.University.Filmark.Web.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public new CreateUserRequest User { get; set; }

        private readonly UserService _userService;

        public RegistrationModel(UserService userService)
        {
            _userService = userService;
        }

        public List<string> Months { get; set; }

        public List<int> Days { get; set; }

        public List<int> Years { get; set; }

        public void OnGet()
        {
            Months = Enumerable.Range(1, 12).Select(m => new DateTime(200, m, 1)).Select(d => d.ToString("MMMM")).ToList();
            Days = Enumerable.Range(1, 31).ToList();
            Years = Enumerable.Range(DateTime.Now.Year - 100, 101).ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            int year = User.SelectedYear;
            int month = DateTime.ParseExact(User.SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            int day = User.SelectedDay;

            string userHashedPassword = HashPassword(User.Password);

            await _userService.Create(new User()
            {
                Name = User.Name,
                UserName = User.UserName,
                EmailAddress = User.EmailAddress,
                Password = userHashedPassword,
                Birthday = new DateTime(year, month, day),
            });

            return new RedirectToPageResult("/Logining");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword;
            }
        }
    }
}
