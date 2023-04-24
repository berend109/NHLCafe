using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Helpers;

namespace WebdevProjectStarterTemplate.Pages
{
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            Session.IsLoggedIn = false;
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}