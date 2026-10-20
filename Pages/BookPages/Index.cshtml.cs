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

    public async Task OnGetAsync()
    {
        Book = await _context.Book.Include(b => b.Publisher).ToListAsync();
    }
}
