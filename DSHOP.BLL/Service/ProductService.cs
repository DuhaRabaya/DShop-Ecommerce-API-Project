using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository , IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var path=await _fileService.UploadFile(request.MainImage);
                product.MainImage = path;
            }
            if (request.SubImages != null) {
                product.SubImages= new List<ProductImage>();
                foreach (var image in request.SubImages) {

                    var path = await _fileService.UploadFile(image);
                    product.SubImages.Add(new ProductImage{ ImageName = path });
                }
            }
            await _productRepository.AddAsync(product);           

            return product.Adapt<ProductResponse>();
        }
        public async Task<List<ProductResponse>> GetAllAsyncForAdmin()
        {
            var products = await _productRepository.GetAllAsync();
            var response=products.Adapt<List<ProductResponse>>();
            return response;
        }

        public async Task<List<ProductUserResponse>> GetAllAsyncForUser([FromQuery] string lang = "en")
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<ProductUserResponse>>();
            return response;

        }

        public async Task<ProductUserDetails> GetProductDetailsForUser(int id, string lang = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);
           var response = product.BuildAdapter().AddParameters("lang", lang).AdaptToType<ProductUserDetails>();

            return response;
        }
    }
}
