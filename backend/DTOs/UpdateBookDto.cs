namespace PLivres.DTOs
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageCover { get; set; }
    }
}
