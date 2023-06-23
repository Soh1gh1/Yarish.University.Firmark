using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Yarish.University.Filmark.Models.Frontend;
using Yarish.University.Filmark.Database.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Yarish.University.Filmark.Database.Services.ApplicationUser;


namespace Yarish.University.Filmark.Web.Pages
{
    public class LoginingModel : PageModel
    {
        [BindProperty]
        public new LoginingRequest User { get; set; }

        private readonly UserService _userService;

        public LoginingModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _userService.EmailAndPasswordMatch(User?.EmailAddress, User?.Password))
            {
                HttpContext.Session.SetString("userEmailAddress", User.EmailAddress);
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email address or password.");
                return Page();
            }
        }
    }
}
