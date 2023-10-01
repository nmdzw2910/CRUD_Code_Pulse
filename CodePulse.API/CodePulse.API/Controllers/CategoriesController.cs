using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await categoryRepository.CreateAsync(category);

            // Map Domain Model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("No categories found.");
            }

            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            }).ToList();

            return Ok(categoryDtos);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            // Find the category by ID
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Map the category to DTO
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(categoryDto);
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequestDto request)
        {
            // Find the category by ID
            var existingCategory = await categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Update the properties of the existing category
            existingCategory.Name = request.Name ?? existingCategory.Name;
            existingCategory.UrlHandle = request.UrlHandle ?? existingCategory.UrlHandle;

            // Call the repository to update the category
            await categoryRepository.UpdateAsync(existingCategory);

            // Map the updated category to DTO
            var updatedCategoryDto = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };

            return Ok(updatedCategoryDto);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            // Find the category by ID
            var existingCategory = await categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Call the repository to delete the category
            await categoryRepository.DeleteAsync(existingCategory);

            // You can return a success message or an appropriate response
            return Ok($"Category {existingCategory.Name} has been deleted.");
        }
    }
}
