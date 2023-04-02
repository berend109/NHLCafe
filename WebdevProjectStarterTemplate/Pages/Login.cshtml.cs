using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        public void OnGet()
        {
        }
    }
}
