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

		public void OnGet() {}

		public IActionResult OnPost()
		{
			User existinUser = new UserRepository().Get(user.Email);

			if (existinUser == null && user.Password == user.Password2) 
			{
				new UserRepository().Add(user.Name, user.Email, Hash.HashPassword(user.Password));
			}

			return Redirect("/index");
		}
	}
}
