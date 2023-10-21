using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">IProductRepository.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductDto request)
        {
            var product = mapper.Map<Product>(request);
            product.CreatedAt = DateTime.Now;
            await productRepository.CreateAsync(product);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> Update(Product existingProduct, ProductDto request)
        {
            mapper.Map(request, existingProduct);

            existingProduct.UpdatedAt = DateTime.Now;

            await productRepository.UpdateAsync(existingProduct);
            return mapper.Map<ProductDto>(existingProduct);
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
