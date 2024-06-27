namespace CodePluse.API.Model.Dto
{
    public class BlogPostResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }
        public List<CreateCategoryDto>? Categories { get; set; }

    }
}
