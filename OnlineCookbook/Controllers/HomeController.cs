using Microsoft.AspNetCore.Mvc;
using OnlineCookbook.Models;
using OnlineCookbook.Data;
using System.Linq;

namespace OnlineCookbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var latestRecipes = _context.Recipes.OrderByDescending(r => r.Id).Take(5).ToList();
            return View(latestRecipes);
        }
    }
}
