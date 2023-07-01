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

    // get products from database based on selected category
    private List<Product> FilterProducts()
    {
        return Products.Where(p => p.CategoryId == SelectedCategoryId)
            .OrderBy(p => p.Name).ToList();
    }

    public void OnGet() { }

    public void OnPost(string DrinkName, string action, int Amount)
    {
        Products = FilterProducts();
        var selectedProduct = string.Empty;

        if (SelectedProductId != 0)
        {
            //kijken of we het product kunnen achterhalen als het er nog niet is
            if (string.IsNullOrEmpty(DrinkName))
            {
                selectedProduct = Products.Where(p => p.ProductId == SelectedProductId).FirstOrDefault().Name;
            }
            //of gewoon aannemen als het er al is
            else { selectedProduct = DrinkName; }
            //kijken of het product al in de sessie staat, zo ja pak dan de opgeslagen count van et bepaalde product
            if (HttpContext.Session.GetInt32(selectedProduct) is not null && Amount == 0)
            {
                Amount = (int)HttpContext.Session.GetInt32(selectedProduct);
            }
            //toevoegen van een product
            if (action == "add")
            {
                HttpContext.Session.SetInt32(selectedProduct, Amount + 1);
            }
            //verwijderen van een product
            else if (action == "remove")
            {
                HttpContext.Session.SetInt32(selectedProduct, Amount - 1);
            }
            //helemaal verwijderen van het type product, ook als de count 0 word
            else if (action == "delete")
            {
                HttpContext.Session.Remove(selectedProduct);
            }
            if (HttpContext.Session.GetInt32(selectedProduct) <= 0)
            {
                HttpContext.Session.Remove(selectedProduct);
            }
        }
    }
}