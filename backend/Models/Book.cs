using System.ComponentModel.DataAnnotations;

namespace PLivres.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }
        public string ImageCover { get; set; } // Chemin de l'image

        public DateTime PublishedDate { get; set; }

    }
}
