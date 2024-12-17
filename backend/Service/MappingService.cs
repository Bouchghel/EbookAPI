using PLivres.DTOs;
using PLivres.Models;

public interface IBookMappingService
{
    BookDto MapToBookDto(Book book);
    Book MapToBook(CreateBookDto createBookDto);
    void UpdateBookFromDto(UpdateBookDto updateBookDto, Book book);
}

public class BookMappingService : IBookMappingService
{
    public BookDto MapToBookDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            ImageCover = book.ImageCover  // On retourne l'image actuelle
        };
    }

    public Book MapToBook(CreateBookDto createBookDto)
    {
        return new Book
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
            Description = createBookDto.Description,
            PublishedDate = createBookDto.PublishedDate
        };
    }

    public void UpdateBookFromDto(UpdateBookDto updateBookDto, Book book)
    {
        book.Title = updateBookDto.Title;
        book.Author = updateBookDto.Author;
        book.Description = updateBookDto.Description;

        // Mise à jour de l'image si une nouvelle image est fournie
        if (updateBookDto.ImageCover != null)
        {
            // Si une nouvelle image est envoyée, elle sera mise à jour
            book.ImageCover = updateBookDto.ImageCover.FileName; // ou toute autre logique pour gérer le chemin du fichier
        }
        // Si l'image n'est pas envoyée, l'image existante reste inchangée
    }
}
