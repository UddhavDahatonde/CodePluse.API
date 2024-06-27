using CodePluse.API.Model.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlog(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPost();
    }
}
