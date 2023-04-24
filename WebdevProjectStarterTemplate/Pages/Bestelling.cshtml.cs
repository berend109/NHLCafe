using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages;

public class Bestelling : PageModel
{
    public IEnumerable<Category> Categories { get; set; } = null!;
    
    public void OnGet()
    {
        Categories = new CategoryRepository().GetCategoriesWithProducts();
    }
}