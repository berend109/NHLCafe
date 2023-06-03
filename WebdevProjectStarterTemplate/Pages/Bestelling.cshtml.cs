using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class Bestelling : PageModel
{
	// get categories and products from database
	private List<Category> categories = MainRepository<Category>.Get().OrderBy(x => x.Name).ToList();
	private List<Product> products = MainRepository<Product>.Get().OrderBy(x => x.Name).ToList();
    
	public List<Category> Categories => categories;
	public List<Product> Products => products;
    
    
	[BindProperty(Name = "CategoryId")]
	public int SelectedCategoryId
	{
		get => HttpContext.Session.GetInt32("SelectedCategoryId") ?? 0;
		set
		{
			HttpContext.Session.SetInt32("SelectedCategoryId", value);
			SelectedProductId = 0;
		} 
	}
    
	[BindProperty(Name = "ProductId")]
	public int SelectedProductId
	{
		get => HttpContext.Session.GetInt32("SelectedProductId") ?? 0;
		set => HttpContext.Session.SetInt32("SelectedProductId", value);
	}
    
	// get products from database based on selected category
	private List<Product> FilterProducts()
	{
		return products.Where(p => p.CategoryId == SelectedCategoryId)
			.OrderBy(p => p.Name).ToList();
	}
	
	public void OnGet() {}
    
	public void OnPost(string DrinkName, string action, int Amount)
	{
		products = FilterProducts();
		var selectedProduct = string.Empty;

		if (SelectedProductId == 0) return;
		selectedProduct = string.IsNullOrEmpty(DrinkName) 
			? products.FirstOrDefault(p => p.ProductId == SelectedProductId)?.Name 
			: DrinkName;

		if (HttpContext.Session.GetInt32(selectedProduct) is not null && Amount == 0)
		{
			Amount = (int)HttpContext.Session.GetInt32(selectedProduct);
		}

		switch (action)
		{
			case "add":
				HttpContext.Session.SetInt32(selectedProduct, Amount + 1);
				break;
			case "remove":
				HttpContext.Session.SetInt32(selectedProduct, Amount - 1);
				break;
			case "delete":
				HttpContext.Session.Remove(selectedProduct);
				break;
		}

		if (HttpContext.Session.GetInt32(selectedProduct) <= 0)
		{
			HttpContext.Session.Remove(selectedProduct);
		}
	}
}