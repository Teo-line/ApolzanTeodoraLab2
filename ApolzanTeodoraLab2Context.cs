using Microsoft.EntityFrameworkCore;

public class ApolzanTeodoraLab2Context(DbContextOptions<ApolzanTeodoraLab2Context> options) : DbContext(options)
{
    public DbSet<ApolzanTeodoraLab2.Models.Book> Book { get; set; } = default!;
    public DbSet<ApolzanTeodoraLab2.Models.Publisher> Publisher { get; set; } = default!;

    public DbSet<ApolzanTeodoraLab2.Models.Author> Author { get; set; } = default!;
}
