using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Helpers;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;
using Newtonsoft.Json;


namespace WebdevProjectStarterTemplate.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User? user { get; set; }

        public IActionResult OnGet()
        {
            return Session.IsLoggedIn ? Redirect("/") : Page();
        }

        public IActionResult OnPost()
        {
            User existingUser = new UserRepository().Get(user.Email);

            if (Hash.HashPassword(user.Password) != existingUser.Password) return Page();
            // Clear password for security reasons
            existingUser.Password = "";

            // Serialize user object to JSON string and store in session,
            // this is needed because only strings can be stored in session.
            string serializedUser = JsonConvert.SerializeObject(existingUser);
            HttpContext.Session.SetString("user", serializedUser);

            Session.IsLoggedIn = true;

            return Redirect("/Bestelling");
        }
    }
}
