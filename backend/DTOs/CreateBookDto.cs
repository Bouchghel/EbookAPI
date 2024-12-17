namespace PLivres.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }

        public IFormFile ImageCover { get; set; }
    }
}
