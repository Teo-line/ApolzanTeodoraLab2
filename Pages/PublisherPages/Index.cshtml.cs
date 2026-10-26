using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApolzanTeodoraLab2.Models;
using ApolzanTeodoraLab2.Models.ViewModels;

namespace ApolzanTeodoraLab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly ApolzanTeodoraLab2Context _context;

    public IndexModel(ApolzanTeodoraLab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public PublisherIndexData PublisherData { get; set; } 
    public int PublisherID { get; set; }
    public int BookID { get; set; }


    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();
        PublisherData.Publishers = await _context.Publisher
            .Include(i => i.Books)
            .ThenInclude(i => i.Author)
            .AsNoTracking()
            .OrderBy(i => i.PublisherName)
            .ToListAsync();

        if(id != null)
        {
            PublisherID = id.Value;
            Publisher publisher = PublisherData.Publishers
                .Where(i => i.ID == PublisherID).Single();
            PublisherData.Books = publisher.Books;
        }
    }
}
