
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApolzanTeodoraLab2.Models;

namespace ApolzanTeodoraLab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public EditModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Borrowing = await _context.Borrowing
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Borrowing == null)
        {
            return NotFound();
        }

        await PopulateDropDownLists();

        return Page();
    }

    private async Task PopulateDropDownLists()
    {
        var members = await _context.Member.ToListAsync();

        ViewData["MemberID"] = new SelectList(
            members, "ID", "FullName",
            Borrowing.MemberID);

        var books = await _context.Book
            .Include(b => b.Author)
            .Select(b => new
            {
                b.ID,
                BookFullName = b.Title + " - " +
                    (b.Author != null
                        ? b.Author.FirstName + " " + b.Author.LastName
                        : "Unknown author")
            })
            .ToListAsync();

        ViewData["BookID"] = new SelectList(
            books, "ID", "BookFullName",
            Borrowing.BookID);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropDownLists();
            return Page();
        }

        var borrowingToUpdate = await _context.Borrowing
            .FirstOrDefaultAsync(b => b.ID == Borrowing.ID);

        if (borrowingToUpdate == null)
        {
            return NotFound();
        }

        borrowingToUpdate.MemberID = Borrowing.MemberID;
        borrowingToUpdate.BookID = Borrowing.BookID;
        borrowingToUpdate.ReturnDate = Borrowing.ReturnDate;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
