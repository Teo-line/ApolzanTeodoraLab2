
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

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulateDropDownLists();
        return Page();
    }

    private async Task PopulateDropDownLists()
    {
        var bookList = await _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " +
                    (x.Author != null
                        ? x.Author.FirstName + " " + x.Author.LastName
                        : "Unknown author")
            })
            .ToListAsync();

        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName");

        var members = await _context.Member.ToListAsync();

        ViewData["MemberID"] = new SelectList(
            members, "ID", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropDownLists();
            return Page();
        }

        _context.Borrowing.Add(Borrowing);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
