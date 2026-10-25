using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;

namespace ApolzanTeodoraLab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public IndexModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }


    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();
        BookD.Books = await _context.Book.
            Include(b => b.Publisher).
            Include(b => b.Author).
            Include(b => b.BookCategories).
            ThenInclude(bc => bc.Category).
            AsNoTracking().OrderBy(b => b.Title).ToListAsync();

        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books.Where(b => b.ID == id.Value).Single();
            BookD.Categories = book.BookCategories.Select(bc => bc.Category);
        }
    }
}
