using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages;

public class SinglePayment : PageModel
{
    // Get all products from the database and order them by name.
    private readonly List<Product> products = MainRepository<Product>.Get().OrderBy(p => p.Name).ToList();
    public List<Product> Products => products;

    public IActionResult OnPost()
    {
        // clear session with the products in the cart
        HttpContext.Session.Keys.ToList().ForEach(key => HttpContext.Session.Remove(key));
        return RedirectToPage("/Index");
    }

    public void OnGet()
    {
        if (HttpContext.Session.Keys.Any()) { }
    }
}