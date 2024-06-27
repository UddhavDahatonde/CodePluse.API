using CodePluse.API.Model.Domain;

namespace CodePluse.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<IEnumerable<Category>> getAllCategory();
        Task<Category> getCategoryById(Guid id);
        Task<Category> EditCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
