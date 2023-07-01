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
		foreach (var v in ProductsToPay.Keys)
		{
			HttpContext.Session.SetInt32(v, HttpContext.Session.GetInt32(v).Value - ProductsToPay[v]);

			if (HttpContext.Session.GetInt32(v) != 0) continue;
			HttpContext.Session.Remove(v);
			ProductsToPay.Remove(v);
		}

		foreach (var key in ProductsToPay.Keys)
		{
			ProductsToPay[key] = 0;
		}
	}
}