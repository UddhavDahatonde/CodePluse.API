using System.ComponentModel.DataAnnotations;

namespace CodePluse.API.Model.Domain
{
    public class Category
    {
        // [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(500)]
        public string? UrlHandler { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }

    }
}
