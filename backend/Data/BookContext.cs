using Microsoft.EntityFrameworkCore;
using PLivres.Models;

namespace PLivres.Data
{
    // Permet d'interagir avec la table des livres dans la base de données
    public class BookContext:DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        // Représente la table des livres dans la base de données
        public DbSet<Book> Books { get; set; }
    }
}
