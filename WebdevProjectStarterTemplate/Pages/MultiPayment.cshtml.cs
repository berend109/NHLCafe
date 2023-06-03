using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class MultiPayment : PageModel
{

	private readonly List<Product> products = MainRepository<Product>.Get().ToList();
	public List<Product> Products => products;
	// dictionary to store the products to pay with key and value
	// key = product name, value = amount of products
	// this is for me the easiest way to store the products to pay (I think)
	public static Dictionary<string, int> ProductsToPay = new();

	public void OnGet()
	{
		HttpContext.Session.Remove("user");
		foreach (var key in HttpContext.Session.Keys)
		{
			ProductsToPay.Add(key, 0);
		}
	}

	public void OnPostAdd(string drinkName, string action)
	{
		switch (action)
		{
			case "min" when ProductsToPay[drinkName] == 0:
				return;
			case "min":
				ProductsToPay[drinkName]--;
				break;
			default:
			{
				// ! to supress warning (Rider recommended this)
				if (HttpContext.Session.GetInt32(drinkName)!.Value == ProductsToPay[drinkName])
				{
					return;
				}

				ProductsToPay[drinkName]++;
				break;
			}
		}
	}

	public void OnPostPay()
	{
		foreach (var key in ProductsToPay.Keys)
		{
			HttpContext.Session.SetInt32(key, HttpContext.Session.GetInt32(key)!.Value - ProductsToPay[key]);

			if (HttpContext.Session.GetInt32(key) != 0) continue;
			HttpContext.Session.Remove(key);
			ProductsToPay.Remove(key);
		}

		foreach (var key in ProductsToPay.Keys.Where(
			         key => HttpContext.Session.GetInt32(key) == null))
		{
			ProductsToPay[key] = 0;
		}
	}
}