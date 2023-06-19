﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Yarish.University.Filmark.Database.Interfaces;
using Yarish.University.Filmark.Models.Database;
using Yarish.University.Filmark.Database.Services;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Yarish.University.Filmark.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class UserService : PageModel
    {
        [BindProperty]
        public new CreateUserRequest User { get; set; }

        private readonly Yarish.University.Filmark.Database.Services.UserService _userService;

        public UserService(Yarish.University.Filmark.Database.Services.UserService.cs userService)
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

            // Check if username already exists
            if (await _userService.UserNameExists(User.UserName))
            {
                ModelState.AddModelError("User.UserName", "Username already exists.");
                OnGet();
                return Page();
            }

            // Check if email already exists
            else if (await _userService.EmailExists(User.EmailAddress))
            {
                ModelState.AddModelError("User.EmailAddress", "Current email already in use.");
                OnGet();
                return Page();
            }

            int year = User.SelectedYear;
            int month = DateTime.ParseExact(User.SelectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            int day = User.SelectedDay;

            string userHashedPassword = _userService.HashPassword(User.Password);

            await _userService.Create(new User()
            {
                Name = User.Name,
                UserName = User.UserName,
                EmailAddress = User.EmailAddress,
                Password = userHashedPassword,
                Birthday = new DateTime(year, month, day),
            });

            return new RedirectToPageResult("/LoginUser");
        }
    }
}