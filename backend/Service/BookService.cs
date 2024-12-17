using Microsoft.EntityFrameworkCore;
using PLivres.Data;
using PLivres.DTOs;

namespace PLivres.Service
{
    // Interface qui définit les méthodes de service pour gérer les livres
    public interface IBookService
    {
        Task<BookDto> AddBookAsync(CreateBookDto createBookDto); //Ajouter un livre
        Task UpdateBookAsync(int id, UpdateBookDto updateBookDto); //Mettre à jour un livre
        Task DeleteBookAsync(int id); //Supprimer un livre
        Task<IEnumerable<BookDto>> SearchBooksAsync(string search); // Rechercher des livres par titre ou auteur
        Task<BookDto> GetBookByIdAsync(int id); // Récupérer un livre par son id
        Task<IEnumerable<BookDto>> GetBooksAsync(); // Récupérer tous les livres en ordre
    }

    public class BookService : IBookService
    {
        private readonly BookContext _context;
        private readonly IBookMappingService _mappingService;
        private readonly FileService _fileService;

        public BookService(BookContext context, IBookMappingService mappingService, FileService fileService)
        {
            _context = context;
            _mappingService = mappingService;
            _fileService = fileService;
        }

        public async Task<BookDto> AddBookAsync(CreateBookDto createBookDto)
        {
            // Enregistrer l'image
            string imagePath = null;
            if (createBookDto.ImageCover != null)
            {
                var uploadResult = await _fileService.SaveFileAsync(createBookDto.ImageCover);
                if (!uploadResult.Success)
                    throw new Exception(uploadResult.ErrorMessage);
                imagePath = uploadResult.FilePath;
            }

            var book = _mappingService.MapToBook(createBookDto);
            book.ImageCover = imagePath;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return _mappingService.MapToBookDto(book);
        }

        public async Task UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            _mappingService.UpdateBookFromDto(updateBookDto, book);

            if (updateBookDto.ImageCover != null)
            {
                var uploadResult = await _fileService.SaveFileAsync(updateBookDto.ImageCover);
                if (!uploadResult.Success)
                    throw new Exception(uploadResult.ErrorMessage);
                book.ImageCover = uploadResult.FilePath;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string search)
        {
            var books = await _context.Books
                .Where(b => string.IsNullOrEmpty(search) ||
                            EF.Functions.Like(b.Title, $"%{search}%") ||
                            EF.Functions.Like(b.Author, $"%{search}%"))
                .ToListAsync();

            return books.Select(b => _mappingService.MapToBookDto(b));
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            return _mappingService.MapToBookDto(book);
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var books = await _context.Books.OrderBy(b => b.Title).ToListAsync();
            return books.Select(b => _mappingService.MapToBookDto(b));
        }
    }

}
