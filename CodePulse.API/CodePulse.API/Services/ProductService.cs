using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">IProductRepository.</param>
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDto> Create(ProductDto request)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Brand = request.Brand,
                Category = request.Category,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await productRepository.CreateAsync(product);

            var response = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Brand = product.Brand,
                Category = product.Category,
            };
            return response;
        }

        public async Task<ProductDto> Update(Product existingProduct, ProductDto request)
        {
            existingProduct.Name = request.Name ?? existingProduct.Name;
            existingProduct.Description = request.Description ?? existingProduct.Description;
            existingProduct.Price = request.Price ?? existingProduct.Price;
            existingProduct.Stock = request.Stock ?? existingProduct.Stock;
            existingProduct.Brand = request.Brand ?? existingProduct.Brand;  
            existingProduct.Category = request.Category ?? existingProduct.Category;
            existingProduct.UpdatedAt = DateTime.Now;

            await productRepository.UpdateAsync(existingProduct);

            var response = new ProductDto
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                Stock = existingProduct.Stock,
                Brand = existingProduct.Brand,
                Category = existingProduct.Category,
            };

            return response;
        }

        public async Task<ProductDto> Upsert(ProductDto product)
        {
            var existingProduct = await productRepository.GetByIdAsync(product.Id);

            if (product.Id != Guid.Empty && existingProduct != null)
            {
                return await Update(existingProduct, product);
            }
            return await Create(product);
        }
    }
}
