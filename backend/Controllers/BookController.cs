using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLivres.Data;
using PLivres.DTOs;
using PLivres.Models;

namespace PLivres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookContext _context;
        private readonly IBookMappingService _mappingService;

        public BookController(BookContext context, IBookMappingService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        // Ajouter un nouveau livre avec une image
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook([FromForm] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Enregistrer l'image
            string imagePath = null;
            if (createBookDto.ImageCover != null)
            {
                var uploadResult = await SaveFileAsync(createBookDto.ImageCover);
                if (!uploadResult.Success)
                    return BadRequest(uploadResult.ErrorMessage);

                imagePath = uploadResult.FilePath; // Chemin relatif, ex : "/uploads/filename.png"
            }

            var book = _mappingService.MapToBook(createBookDto);
            book.ImageCover = imagePath; // Enregistrez le chemin relatif en base de données

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Créez une URL complète pour la réponse DTO
            var fullImageUrl = !string.IsNullOrEmpty(book.ImageCover)
                ? $"{Request.Scheme}://{Request.Host}{book.ImageCover}" // URL complète
                : null;

            var bookDto = _mappingService.MapToBookDto(book);
            bookDto.ImageCover = fullImageUrl; // utiliser l'URL complète ici

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            // Mise à jour des propriétés
            _mappingService.UpdateBookFromDto(updateBookDto, book);

            // Si une nouvelle image est fournie, on la met à jour
            if (updateBookDto.ImageCover != null)
            {
                var uploadResult = await SaveFileAsync(updateBookDto.ImageCover);
                if (!uploadResult.Success)
                    return BadRequest(new { message = "Image upload failed", error = uploadResult.ErrorMessage });

                book.ImageCover = uploadResult.FilePath;
            }
            else
            {
                // Si aucune image n'est fournie, on conserve l'image existante
                book.ImageCover = book.ImageCover; // Pas de modification de l'image
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the book", error = ex.Message });
            }

            return NoContent();
        }


        // Télécharger une image de livre
        /*[HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            var uploadResult = await SaveFileAsync(file);
            if (!uploadResult.Success)
                return BadRequest(uploadResult.ErrorMessage);

            book.ImageCover = uploadResult.FilePath;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = $"{Request.Scheme}://{Request.Host}/{book.ImageCover}" });
        }
        */
        // Méthode pour sauvegarder un fichier
        private async Task<(bool Success, string FilePath, string ErrorMessage)> SaveFileAsync(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                return (false, null, "Invalid file type. Only JPG and PNG are allowed.");

            var uploadDirectory = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadDirectory, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return (false, null, $"An error occurred while saving the file: {ex.Message}");
            }

            // Ne retournez que le chemin relatif ici
            var relativePath = $"/uploads/{fileName}";
            return (true, relativePath, null);
        }



        // Supprimer un livre
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Rechercher des livres par titre ou auteur
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string search)
        {
            // On crée une requête de base qui récupère tous les livres
            IQueryable<Book> query = _context.Books;

            // Si un terme de recherche est fourni, on l'applique
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => EF.Functions.Like(b.Title, $"%{search}%") ||
                                         EF.Functions.Like(b.Author, $"%{search}%"));
            }

            // Récupérer tous les livres ou ceux filtrés par le terme de recherche
            var books = await query
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageCover = string.IsNullOrEmpty(b.ImageCover) ? null : $"{Request.Scheme}://{Request.Host}/{b.ImageCover}"
                })
                .ToListAsync();

            return Ok(books);
        }

        // Obtenir la liste des livres triée par titre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _context.Books
                .OrderBy(b => b.Title)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageCover = string.IsNullOrEmpty(b.ImageCover) ? null : $"{Request.Scheme}://{Request.Host}/{b.ImageCover}"
                })
                .ToListAsync();

            return Ok(books);
        }

        // Obtenir un livre par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ImageCover = string.IsNullOrEmpty(book.ImageCover) ? null : $"{Request.Scheme}://{Request.Host}/{book.ImageCover}"
            };

            return Ok(bookDto);
        }

        }
}
