using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class MultiPayment : PageModel
{
	public ILogger<IndexModel> Logger1 { get; }
	private readonly List<Product> _products = MainRepository<Product>.Get().ToList();
	public List<Product> Products => _products;
	// dictionary to store the products to pay with key and value
	// key = product name, value = amount of products
	// this is for me the easiest way to store the products to pay (I think)
	public static Dictionary<string, int> ProductsToPay = new();
	public MultiPayment(ILogger<IndexModel> logger)
	{
		Logger1 = logger;
	}

	public void OnGet()
	{
		foreach (var key in HttpContext.Session.Keys)
		{
			if (ProductsToPay.ContainsKey(key)) continue;
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
			if (key == "user")
			{
				continue;
			}
			Console.WriteLine(HttpContext.Session.GetInt32(key).Value);
			HttpContext.Session.SetInt32(key, HttpContext.Session.GetInt32(key).Value - ProductsToPay[key]);

			if (HttpContext.Session.GetInt32(key) != 0) continue;
			HttpContext.Session.Remove(key);
			ProductsToPay.Remove(key);
		}

		foreach (var key in ProductsToPay.Keys)
		{
			ProductsToPay[key] = 0;
		}
	}
}