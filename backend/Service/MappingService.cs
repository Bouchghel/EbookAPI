using PLivres.DTOs;
using PLivres.Models;

public interface IBookMappingService
{
    // Méthode pour mapper un objet Book vers un BookDto
    BookDto MapToBookDto(Book book);

    // Méthode pour mapper un CreateBookDto vers un objet Book
    Book MapToBook(CreateBookDto createBookDto);

    // Méthode pour mettre à jour un objet Book à partir d'un UpdateBookDto
    void UpdateBookFromDto(UpdateBookDto updateBookDto, Book book);
}

public class BookMappingService : IBookMappingService
{
    private readonly IConfiguration _configuration;
    public BookMappingService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public BookDto MapToBookDto(Book book)
    {
        var baseUrl = _configuration["ImageBaseUrl"];
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            ImageCover = $"{baseUrl}/{book.ImageCover}",

        };
    }

    public Book MapToBook(CreateBookDto createBookDto)
    {
        return new Book
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
            Description = createBookDto.Description,
            PublishedDate = createBookDto.PublishedDate,
        };
    }

    public void UpdateBookFromDto(UpdateBookDto updateBookDto, Book book)
    {
        book.Title = updateBookDto.Title;
        book.Author = updateBookDto.Author;
        book.Description = updateBookDto.Description;

        // Mise à jour de l'image si une nouvelle image est fournie
        //if (updateBookDto.ImageCover != null)
        //{
            // Si une nouvelle image est envoyée, elle sera mise à jour
            //book.ImageCover = updateBookDto.ImageCover.FileName; // ou toute autre logique pour gérer le chemin du fichier
        //}
        // Si l'image n'est pas envoyée, l'image existante reste inchangée
    }
}
