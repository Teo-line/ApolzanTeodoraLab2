using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;

namespace ApolzanTeodoraLab2.Pages.AuthorPages;

public class DetailsModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;
    public DetailsModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

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
}
