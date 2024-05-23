using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IS3Service _s3Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService(IProductRepository productRepository, IMapper mapper, IS3Service s3Service)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
            this._s3Service = s3Service;
        }

        public async Task<ProductDto> Create(ProductDto request, IFormFileCollection images)
        {
            var product = _mapper.Map<Product>(request);
            product.CreatedAt = DateTime.Now;
            product.ProductImages = await UploadImages(images);
            await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> Update(Product existingProduct, ProductDto request, IFormFileCollection images)
        {
            _mapper.Map(request, existingProduct);

            existingProduct.UpdatedAt = DateTime.Now;
            if (images.Count > 0)
            {
                existingProduct.ProductImages = await UploadImages(images);
            }
            await _productRepository.UpdateAsync(existingProduct);
            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<ProductDto> Upsert(ProductDto product, IFormFileCollection images)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);

            if (product.Id != Guid.Empty && existingProduct != null)
            {
                return await Update(existingProduct, product, images);
            }
            return await Create(product, images);
        }

        public async Task<List<ProductImage>> UploadImages(IFormFileCollection? files)
        {
            var uploadedImages = new List<ProductImage>();

            if (files == null || files.Count == 0)
            {
                return uploadedImages;
            }

            const string productImagePath = "productImages/";

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var imageUrl = await _s3Service.UploadImageToS3(file, productImagePath);

                    var productImage = new ProductImage
                    {
                        Url = imageUrl
                    };

                    uploadedImages.Add(productImage);
                }
            }

            return uploadedImages;
        }
    }
}
