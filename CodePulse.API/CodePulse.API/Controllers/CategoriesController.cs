using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieve a list of all categories.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get category by name.
        /// </summary>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategory(string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Add a new category.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] string name)
        {
            var createdCategory = await _categoryRepository.CreateAsync(name);
            return CreatedAtAction(nameof(GetCategory), new { name = createdCategory }, createdCategory);
        }

        /// <summary>
        /// Update a category to new name by its old name.
        /// </summary>
        [HttpPut("{oldName}")]
        public async Task<IActionResult> UpdateCategory(string oldName, [FromBody] string newName)
        {
            var updatedCategory = await _categoryRepository.UpdateAsync(oldName, newName);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        /// <summary>
        /// Delete a category by name
        /// </summary>
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCategory(string name)
        {
            var deletedCategory = await _categoryRepository.DeleteAsync(name);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
