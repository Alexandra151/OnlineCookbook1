using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCookbook.Data; 
using OnlineCookbook.Models; 
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class RecipeController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public RecipeController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var recipes = await _context.Recipes.Include(r => r.Category).Include(r => r.User).ToListAsync();
        return View(recipes);
    }

    public async Task<IActionResult> Create()
    {
        ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            Console.WriteLine("❌ Błąd: Brak zalogowanego użytkownika!");
            ModelState.AddModelError("", "You must be logged in to add a recipe.");
            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View(recipe);
        }

        recipe.UserId = user.Id;
        recipe.User = user;

        Console.WriteLine($"✅ Ustawiono UserId: {recipe.UserId}");

        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("❌ Validation Error: " + error.ErrorMessage);
            }

            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View(recipe);
        }

        try
        {
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            Console.WriteLine("✅ Przepis został zapisany poprawnie!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Błąd podczas zapisywania przepisu: " + ex.Message);
        }

        return RedirectToAction("Index", "Recipe");
    }

    // GET: Recipe/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipe.CategoryId);
        return View(recipe);
    }

    // POST: Recipe/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Rating,CategoryId,UserId")] Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipe.CategoryId);
        return View(recipe);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipes.Any(e => e.Id == id);
    }
}
