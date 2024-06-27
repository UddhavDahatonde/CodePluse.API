using CodePluse.API.Model.Domain;
using CodePluse.API.Model.Dto;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;
        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpPost, Route("/CreatePost")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Post(CreateBlogPost blogPostDto)
        {
            if (blogPostDto == null)
            {
                throw new ArgumentNullException(nameof(blogPostDto));
            }

            // Create a new BlogPost instance
            BlogPost blogPost = new BlogPost()
            {
                Author = blogPostDto.Author,
                Content = blogPostDto.Content,
                FeaturedImageUrl = blogPostDto.FeaturedImageUrl,
                IsVisible = blogPostDto.IsVisible,
                PublishedDate = blogPostDto.PublishedDate,
                ShortDescription = blogPostDto.ShortDescription,
                Title = blogPostDto.Title,
                UrlHandler = blogPostDto.UrlHandler,
                Categories = new List<Category>()
            };
            try
            {
                // Iterate over the provided category IDs and add existing categories to the BlogPost
                foreach (var categoryId in blogPostDto.Categories ?? throw new ArgumentException(nameof(blogPostDto.Categories)))
                {
                    var existingCategory = await _categoryRepository.getCategoryById(categoryId);
                    if (existingCategory != null && !blogPost.Categories.Any(c => c.Id == existingCategory.Id))
                    {
                        blogPost.Categories.Add(existingCategory);
                    }
                }

                // Attempt to create the blog post using repository method
                var createdBlog = await _blogPostRepository.CreateBlog(blogPost);

                if (createdBlog == null)
                {
                    return BadRequest("Failed to create the blog post.");
                }

                return Ok(createdBlog);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error creating blog post: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet, Route("/GetAllPost")]
        public async Task<IActionResult> GetAll()
        {
            var postList = await _blogPostRepository.GetAllBlogPost();
            List<BlogPostResponse> posts = new List<BlogPostResponse>();
            if (postList is not null)
            {
                BlogPostResponse blogPost = new();
                CreateCategoryDto categoryDto = new();
                foreach (var post in postList)
                {
                    blogPost = new BlogPostResponse()
                    {
                        Id = post.Id,
                        Author = post.Author,
                        Content = post.Content,
                        FeaturedImageUrl = post.FeaturedImageUrl,
                        IsVisible = post.IsVisible,
                        PublishedDate = post.PublishedDate,
                        ShortDescription = post.ShortDescription,
                        Title = post.Title,
                        UrlHandler = post.UrlHandler,
                        Categories = new()
                    };
                    if (post.Categories != null)
                    {
                        foreach (var item in post.Categories)
                        {
                            categoryDto = new CreateCategoryDto()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                UrlHandler = item.UrlHandler
                            };
                            blogPost.Categories.Add(categoryDto);
                        }
                    }
                    posts.Add(blogPost);
                }
            }
            return Ok(posts);
        }
    }
}
