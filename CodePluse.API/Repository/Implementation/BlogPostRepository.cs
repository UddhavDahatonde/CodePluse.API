using CodePluse.API.DataContext;
using CodePluse.API.Model.Domain;
using CodePluse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repository.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BlogPost> CreateBlog(BlogPost blogPost)
        {
            await _dbContext.Blogs.AddAsync(blogPost);
            var result = await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPost()
        {
            return await _dbContext.Blogs.Include(x => x.Categories).AsNoTracking().ToListAsync();
        }
    }
}
