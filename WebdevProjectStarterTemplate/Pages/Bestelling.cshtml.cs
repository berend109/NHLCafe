using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;
namespace WebdevProjectStarterTemplate.Pages;

public class Bestelling : PageModel
{
    // get categories and products from database
    public List<Category> Categorys { get; } = MainRepository<Category>.Get().OrderBy(x => x.Name).ToList();

    public List<Product> Products { get; private set; } = MainRepository<Product>.Get().OrderBy(x => x.Name).ToList();


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

    /// <summary>
    /// Get products from database based on selected category
    /// </summary>
    /// <returns>filtered products</returns>
    private List<Product> FilterProducts()
    {
        return Products.Where(p => p.CategoryId == SelectedCategoryId)
            .OrderBy(p => p.Name).ToList();
    }

    public void OnGet() { }

    public void OnPost(string DrinkName, string action, int Amount)
    {
        Products = FilterProducts();

        if (SelectedProductId == 0) return;
        var selectedProduct = !string.IsNullOrEmpty(DrinkName) ? DrinkName : 
            Products.FirstOrDefault(p => p.ProductId == SelectedProductId)?.Name;

        if (HttpContext.Session.GetInt32(selectedProduct ?? throw new InvalidOperationException()) is not null && Amount == 0)
        {
            Amount = (int)HttpContext.Session.GetInt32(selectedProduct);
        }
        
        // switch statement because we have 3 buttons
        // add, remove and delete seemed like the best option
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

        if (!(HttpContext.Session.GetInt32(selectedProduct) <= 0)) return;
        HttpContext.Session.Remove(selectedProduct);
    }
}