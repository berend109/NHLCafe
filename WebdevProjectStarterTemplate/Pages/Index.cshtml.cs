using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class IndexModel : PageModel
{
    public List<Product> Products { get; set; } = null!;
    public List<Product> ProductWithCategory { get; set; } = null!;

    public void OnGet()
    {
        Products = MainRepository<Product>.Get().OrderBy(p => p.Name).ToList();
        ProductWithCategory = MainRepository<Product>.Get().OrderBy(p => p.Name).ToList();
    }
}