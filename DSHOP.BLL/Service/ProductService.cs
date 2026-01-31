using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Migrations;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginateResponse<ProductUserResponse>> GetAllAsyncForUser(string lang = "en" ,
            int page=1 , int limit=1 ,string? search=null,
            int? categoryId=null, decimal? minPrice=null, decimal? maxPrice=null,
            string? sortBy=null , bool asc=true)
        {
            var query = _productRepository.Query();
            if(search is not null)
            {
                query = query.Where(p=>p.Translations.Any(t => t.Language == lang && (t.Name.Contains(search) || t.Description.Contains(search))));
            }
            if(categoryId is not null)
            {
                query= query.Where(p=>p.CategoryId==categoryId);
            }
            if(minPrice is not null)
            {
                query=query.Where(p=>p.Price>=minPrice);
            }
            if (maxPrice is not null)
            {
                query = query.Where(p => p.Price <= minPrice);
            }
            if(sortBy is not null)
            {
                sortBy = sortBy.ToLower();

                if (sortBy == "price")
                {
                    query= asc ? query.OrderBy(p=>p.Price) : query.OrderByDescending(p=>p.Price);
                }else if(sortBy == "name")
                {
                    query = asc ? query.OrderBy(p => p.Translations.FirstOrDefault(p => p.Language == lang).Name)
                        : query.OrderByDescending(p => p.Translations.FirstOrDefault(p => p.Language == lang).Name);
                }else if (sortBy == "rate")
                {
                    query=asc? query.OrderBy(P=>P.Rate) : query.OrderByDescending(P=>P.Rate);
                }
            }
            var total= await query.CountAsync();

             query= query.Skip((page-1)*limit).Take(limit);

            var response = query.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<ProductUserResponse>>();
            return new PaginateResponse<ProductUserResponse>
            {
                Total = total,
                Page = page,    
                Limit = limit,
                Data=response
            };

        }

        public async Task<ProductUserDetails> GetProductDetailsForUser(int id, string lang = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);
            if(product is null)
            {
                return new ProductUserDetails()
                {
                    Success = false,
                    Message="product not found!"
                };
            }
            var response = product.BuildAdapter().AddParameters("lang", lang).AdaptToType<ProductUserDetails>();
            response.Success = true;
            return response;
        }
        public async Task<BaseResponse> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            if (product == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "product not found"
                };
            }

            await _productRepository.RemoveAsync(product);

            return new BaseResponse
            {
                Success = true,
                Message = "product deleted successfully"
            };
        }

        public async Task<BaseResponse> UpdateProductAsync(int productId,UpdateProductRequest request)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            if (product == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product not found"
                };
            }
            if (request.Price.HasValue) product.Price=request.Price.Value;
            if (request.Quantity.HasValue)product.Quantity = request.Quantity.Value;
            if (request.Discount.HasValue)product.Discount = request.Discount.Value;
            if (request.CategoryId.HasValue)product.CategoryId = request.CategoryId.Value;

            if (request.MainImage != null)
            {
                var path = await _fileService.UploadFile(request.MainImage);
                product.MainImage = path;
            }

            if (request.SubImages != null)
            { 
                foreach (var image in request.SubImages)
                {
                    var path = await _fileService.UploadFile(image);
                    product.SubImages.Add(new ProductImage { ImageName = path });
                }
            }
            if (request.Translations != null)
            {
                foreach (var translation in request.Translations)
                {
                    var existing = product.Translations.FirstOrDefault(t => t.Language == translation.Language);

                    if (existing is not null)
                    {
                        existing.Name = translation.Name;

                    }
                    else
                    {
                        product.Translations.Add(new ProductTranslations
                        {
                            Name = translation.Name,
                            Language = translation.Language,
                        });
                    }
                }
            }

            await _productRepository.UpdateAsync(product);

            return new BaseResponse
            {
                Success = true,
                Message = "Product updated successfully"
            };
        }
    }
}
