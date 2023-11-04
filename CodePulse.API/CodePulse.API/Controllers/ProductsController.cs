﻿using AutoMapper;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, IProductService productService, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productService = productService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }

            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDtos);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            var productDto = mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        /// <summary>
        /// Create or update a Product. If the optional id is provided in the body, the existing Product with that id is overwritten.
        /// If it is not provided, then a new Product is created, and an id is generated.
        /// </summary>
        /// <param name="product">
        /// Body of a request must contain all necessary data about Product that will be created/updated.
        /// The Id attribute is optional and when provided the existing Product with that id is overwritten.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> UpsertProduct(ProductDto request)
        {
            var response = await productService.Upsert(request);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            await productRepository.DeleteAsync(existingProduct);

            return Ok($"Product {existingProduct.Name} has been deleted.");
        }

        /// <summary>
        /// Deletes multiple products by their unique identifiers.
        /// </summary>
        /// <param name="ids">A collection of unique identifiers for the products to delete.</param>
        [HttpDelete("bulkdelete")]
        public async Task<IActionResult> BulkDeleteProducts([FromBody] List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No product IDs provided for deletion.");
            }

            foreach (var id in ids)
            {
                var existingProduct = await productRepository.GetByIdAsync(id);

                if (existingProduct != null)
                {
                    await productRepository.DeleteAsync(existingProduct);
                }
            }

            return Ok($"Deleted {ids.Count} products.");
        }
        [HttpPost("uploadProductImages")]
        public async Task<IActionResult> UploadProductImages(IFormFileCollection files)
        {
            var uploadedImages = await productService.UploadImages(files);

            if (uploadedImages == null)
            {
                return BadRequest("Error uploading images.");
            }

            return Ok(uploadedImages);
        }
    }
}
