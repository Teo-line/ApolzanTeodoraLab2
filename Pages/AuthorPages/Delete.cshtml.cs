using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;

namespace ApolzanTeodoraLab2.Pages.AuthorPages;

public class DeleteModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public DeleteModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? authorid)
    {
        if (authorid is null)
        {
            return NotFound();
        }

        var author = await _context.Author.FirstOrDefaultAsync(m => m.AuthorId == authorid);
        if (author is null)
        {
            return NotFound();
        }
        else
        {
            Author = author;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? authorid)
    {
        if (authorid is null)
        {
            return NotFound();
        }

        var author = await _context.Author.FindAsync(authorid);
        if (author != null)
        {
            Author = author;
            _context.Author.Remove(Author);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
