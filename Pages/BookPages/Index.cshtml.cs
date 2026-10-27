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

    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }

    public string CurrentFilter { get; set; }


    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        BookD = new BookData();


        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";

        CurrentFilter = searchString;

        BookD.Books = await _context.Book.
            Include(b => b.Publisher).
            Include(b => b.Author).
            Include(b => b.BookCategories).
            ThenInclude(bc => bc.Category).
            AsNoTracking().OrderBy(b => b.Title).ToListAsync();

        if(!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books
        .Where(s =>
            s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
            (s.Author != null &&
             (s.Author.FirstName.Contains(searchString) ||
              s.Author.LastName.Contains(searchString))))
        .ToList();
        }

        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books.Where(b => b.ID == id.Value).Single();
            BookD.Categories = book.BookCategories.Select(bc => bc.Category);
        }

        switch(sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(b => b.Title).ToList();
                break;
            case "Author":
                BookD.Books = BookD.Books.OrderBy(b => b.Author.FullName).ToList();
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(b => b.Author.FullName).ToList();
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(b => b.Title).ToList();
                break;
        }
    }
}
