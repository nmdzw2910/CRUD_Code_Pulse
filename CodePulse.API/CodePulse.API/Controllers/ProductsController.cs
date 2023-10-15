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


        public ProductsController(IProductRepository productRepository, IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }

            var productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Brand = product.Brand,
                Category = product.Category,
                Images = product.Images,
            }).ToList();

            return Ok(productDtos);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Brand = product.Brand,
                Category = product.Category,
                Images = product.Images,
            };

            return Ok(productDto);
        }

        /// <summary>
        /// Creates or updates an Product. If the optional id is provided in the body, the existing Product with that id is overwritten.
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

        // DELETE: api/products/{id}
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

        // DELETE: api/products
        [HttpDelete]
        public async Task<IActionResult> BulkDeleteProducts(List<Guid> ids)
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
    }
}
