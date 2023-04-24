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
            Session session = new Session();

            bool user = Session.CheckIfLoggedIn(HttpContext.Session.GetString("username"));

            return user ? Redirect("/") : Page();
        }

        public IActionResult OnPost()
        {
            User existingUser = new UserRepository().Get(user.Email);

            if (existingUser != null && (Hasj.HasjPassword(user.Password) == existingUser.Password))
            {
                existingUser.Password = "";

                string user = JsonConvert.SerializeObject(existingUser);
                HttpContext.Session.SetString("username", user);

                return Redirect("/Drankenkaart");
            }

            return Page();
        }
    }
}
