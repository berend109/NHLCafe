using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class Drankenkaart : PageModel
{
    public IEnumerable<Product> ProductWithCategory { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;
    public void OnGet()
    {
        ProductWithCategory = new ProductRepository().GetProductWithCategory(); 
        Categories = new CategoryRepository().GetCategoriesWithProducts();
    }
}