using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApolzanTeodoraLab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public CreateModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var bookList = _context.Book.Include(b => b.Author).Select(x => new
        {
            x.ID,
            BookFullName = x.Title + " - " + x.Author.FirstName + " " + x.Author.LastName
        });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName"); 
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
