using System.ComponentModel.DataAnnotations;

namespace CodePluse.API.Model.Dto
{
    public class CreateCategoryDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? UrlHandler { get; set; }
    }
}
