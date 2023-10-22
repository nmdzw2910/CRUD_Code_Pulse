using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace CodePulse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">IProductRepository.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper, ApplicationDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
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

            existingProduct.ProductImages = UpdateImages(existingProduct.ProductImages, request.ProductImages);
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

        private List<ProductImage> UpdateImages(List<ProductImage> existingImages, List<ProductImageDto> updatedImages)
        {
            var imagesToDelete = existingImages.Where(existingImage =>
                !updatedImages.Any(updatedImage => updatedImage.Id == existingImage.Id)).ToList();

            if (imagesToDelete.Any())
            {
                dbContext.ProductImages.RemoveRange(imagesToDelete);
                foreach (var image in imagesToDelete)
                {
                    existingImages.Remove(image);
                }
            }
            
            foreach (var updatedImage in updatedImages)
            {
                if (updatedImage.Id == Guid.Empty)
                {
                    updatedImage.Id = Guid.NewGuid();
                }

                var existingImage = existingImages.FirstOrDefault(image => image.Id == updatedImage.Id);

                if (existingImage != null)
                {
                    mapper.Map(updatedImage, existingImage);
                }
                else
                {
                    existingImages.Add(mapper.Map<ProductImage>(updatedImage));
                }
            }

            return existingImages;
        }
    }
}
