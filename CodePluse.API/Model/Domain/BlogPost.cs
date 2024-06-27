using System.ComponentModel.DataAnnotations;

namespace CodePluse.API.Model.Domain
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(2000)]
        public string? Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string? ShortDescription { get; set; }
        [Required]
        [StringLength(5000)]
        public string? Content { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required]
        [StringLength(500)]
        public string? Author { get; set; }
        [Required]
        public bool IsVisible { get; set; }

        public ICollection<Category>? Categories { get; set; }
    }
}
