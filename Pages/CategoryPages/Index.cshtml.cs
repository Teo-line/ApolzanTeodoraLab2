using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;
using ApolzanTeodoraLab2.Models.ViewModels;

namespace ApolzanTeodoraLab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public IndexModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public CategoryIndexData CategoryD { get; set; }

    public int CategoryID { get; set; }
    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        CategoryD = new CategoryIndexData();

        CategoryD.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .AsNoTracking()
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;

            Category category = CategoryD.Categories
                .Where(c => c.ID == CategoryID)
                .Single();

            CategoryD.Books = category.BookCategories
                .Select(bc => bc.Book)
                .ToList();
        }
    }
}