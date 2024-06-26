﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };

            await _categoryRepository.CreateAsync(category);

            // Map Domain Model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return Ok(response);
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("No categories found.");
            }

            var categoryDtos = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            }).ToList();

            return Ok(categoryDtos);
        }

        // GET: api/categories/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            // Find the category by ID
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Map the category to DTO
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return Ok(categoryDto);
        }

        // PUT: api/categories/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequestDto request)
        {
            // Find the category by ID
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Update the properties of the existing category
            existingCategory.Name = request.Name ?? existingCategory.Name;
            existingCategory.Description = request.Description ?? existingCategory.Description;

            // Call the repository to update the category
            await _categoryRepository.UpdateAsync(existingCategory);

            // Map the updated category to DTO
            var updatedCategoryDto = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                Description = existingCategory.Description,
            };

            return Ok(updatedCategoryDto);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            // Find the category by ID
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Call the repository to delete the category
            await _categoryRepository.DeleteAsync(existingCategory);

            // You can return a success message or an appropriate response
            return Ok($"Category {existingCategory.Name} has been deleted.");
        }
    }
}
