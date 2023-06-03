using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class Overzicht : PageModel
{
	private readonly List<Product> products = MainRepository<Product>.Get().OrderBy(p => p.Name).ToList();
	public List<Product> Products => products;
	public void OnGet()
	{
		HttpContext.Session.Remove("SelectedCategoryId");
		HttpContext.Session.Remove("SelectedProductId");
	}
	
	public void OnPostAdd(string DrinkName, string action)
	{
		int amount = HttpContext.Session.GetInt32(DrinkName) ?? 0;

		if (action == "min")
		{
			if (amount == 0)
			{
				return;
			}

			amount--;
		}
		else
		{
			amount++;
		}

		HttpContext.Session.SetInt32(DrinkName, amount);
		if (HttpContext.Session.GetInt32(DrinkName) == 0) 
			HttpContext.Session.Remove(DrinkName);
	}
}