using Microsoft.AspNetCore.Mvc;
using PLivres.DTOs;
using PLivres.Service;

namespace PLivres.Controllers
{
    // Définition de la route de base pour les actions du contrôleur
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        //l'injection de IBookService
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Action pour ajouter un nouveau livre
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook([FromForm] CreateBookDto createBookDto)
        {
            try
            {
                var bookDto = await _bookService.AddBookAsync(createBookDto);
                return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, bookDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        // Action pour mettre à jour un livre existant
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDto updateBookDto)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, updateBookDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Action pour supprimer un livre
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // Action pour rechercher des livres par titre ou auteur
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string search)
        {
            var books = await _bookService.SearchBooksAsync(search);
            return Ok(books);
        }

        // Action pour récupérer tous les livres (en ordre)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }

        // Action pour récupérer un livre spécifique par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
