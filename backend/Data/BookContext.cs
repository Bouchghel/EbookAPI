using Microsoft.EntityFrameworkCore;
using PLivres.Models;

namespace PLivres.Data
{
    public class BookContext:DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
