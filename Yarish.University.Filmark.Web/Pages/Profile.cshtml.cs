using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yarish.University.Filmark.Models.Database;
using System.Threading.Tasks;
using Yarish.University.Filmark.Database.Services;
using Microsoft.AspNetCore.Http;

namespace Yarish.University.Filmark.Web.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationUser.UserService _userService;

        public ProfileModel(ApplicationUser.UserService userService)
        {
            _userService = userService;
        }

        public User ProfileUser { get; set; }

        public async Task<IActionResult> OnGetAsync([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userEmail = HttpContext.Session.GetString("userEmailAddress"); // Змінено ключ

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToPage("/Logining");
            }

            ProfileUser = await _userService.GetByEmail(userEmail);

            if (ProfileUser == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
