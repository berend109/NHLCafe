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
        public User user { get; set; }

        public IActionResult OnGet()
        {
            return Session.IsLoggedIn ? Redirect("/") : Page();
        }

        public IActionResult OnPost()
        {
            User existingUser = new UserRepository().Get(user.Email);

            if (Hasj.HasjPassword(user.Password) == existingUser.Password)
            {
                // Clear password for security reasons
                existingUser.Password = "";

                // Serialize user object to JSON string and store in session
                string serializedUser = JsonConvert.SerializeObject(existingUser);
                HttpContext.Session.SetString("user", serializedUser);

                // set logged in to true
                Session.IsLoggedIn = true;

                // Redirect to desired page
                return Redirect("/Bestelling");
            }
            // Return current page since login was unsuccessful
            return Page();
        }
    }
}
