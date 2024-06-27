using CodePluse.API.DataContext;
using CodePluse.API.Model.Domain;
using CodePluse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> EditCategory(Category category)
        {

            var existCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existCategory != null)
            {
                _dbContext.Entry(existCategory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
                return category; // Return updated category object if needed
            }
            return null;
        }

        public async Task<IEnumerable<Category>> getAllCategory()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> getCategoryById(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteCategory(Category category)
        {
            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
