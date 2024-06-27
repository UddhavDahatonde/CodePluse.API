using CodePluse.API.Model.Domain;
using CodePluse.API.Model.Dto;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost, Route("/CreateCategory")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            Category category = new Category()
            {
                Name = categoryDto.Name,
                UrlHandler = categoryDto.UrlHandler
            };
            var categoryResponse = await _categoryRepository.CreateCategory(category);
            return Ok(new Category()
            {
                Name = categoryDto.Name,
                UrlHandler = categoryDto.UrlHandler
            });
        }
        [HttpGet, Route("/getAllCategory")]
        public async Task<IEnumerable<CreateCategoryDto>> GetAllCategories()
        {
            var categoryList = await _categoryRepository.getAllCategory();
            var categoryDtoList = new List<CreateCategoryDto>();
            foreach (var category in categoryList)
            {
                categoryDtoList.Add(new CreateCategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandler = category.UrlHandler
                });
            }
            return categoryDtoList;
        }
        [HttpGet, Route("/getCategoryById/{id:Guid}")]
        public async Task<CreateCategoryDto> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.getCategoryById(id);
            var categoryDto = new CreateCategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandler = category.UrlHandler
            };
            return categoryDto;
        }
        [HttpPut, Route("/editCategory/{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<UpdateCategoryDto> EditCategory([FromRoute] Guid id, UpdateCategoryDto categoryDto)
        {
            Category category = new Category()
            {
                Id = id,
                Name = categoryDto.Name,
                UrlHandler = categoryDto.UrlHandler
            };
            var categoryUpdated = await _categoryRepository.EditCategory(category);
            if (categoryUpdated is not null)
            {
                return categoryDto;
            }
            return new();
        }
        [HttpDelete, Route("/DeleteCategory/{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task Delete([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.getCategoryById(id);
            if (existingCategory is not null)
            {
                await _categoryRepository.DeleteCategory(existingCategory);
            }
        }
    }
}
