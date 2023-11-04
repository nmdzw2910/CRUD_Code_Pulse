using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;
        private readonly IS3Service s3Service;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">IProductRepository.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper, ApplicationDbContext dbContext, IS3Service s3Service)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.s3Service = s3Service; 
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

        public async Task<List<ProductImage>> UploadImages(IFormFileCollection files)
        {
            var uploadedImages = new List<ProductImage>();

            if (files == null || files.Count == 0)
            {
                return uploadedImages;
            }

            var productImagePath = "ProductImages/";

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var imageUrl = await s3Service.UploadImageToS3(file, productImagePath);

                    var productImage = new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        Url = imageUrl
                    };

                    uploadedImages.Add(productImage);
                }
            }

            return uploadedImages;
        }


    }
}
