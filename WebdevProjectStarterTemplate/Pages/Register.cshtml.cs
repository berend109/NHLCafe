using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Helpers;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }

        public IActionResult OnPost()
        {
            User existinUser = new UserRepository().Get(user.Email);

            if (existinUser == null && user.Password1 == user.Password2) 
            {
                new UserRepository().Add(user.Name, user.Email, Hasj.HasjPassword(user.Password1));
            }

            return Redirect("/index");
        }
    }
}
