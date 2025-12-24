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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            var cat = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(cat);
            return cat.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllAsyncForAdmin()
        {
            var cats=await _categoryRepository.GetAllAsync();

            var response = cats.Adapt<List<CategoryResponse>>();
            return response;

        }
        public async Task<List<CategoryUserResponse>> GetAllAsyncForUser([FromQuery] string lang = "en")
        {
            var cats = await _categoryRepository.GetAllAsync();
            var response=cats.BuildAdapter().AddParameters("lang" ,lang).AdaptToType<List<CategoryUserResponse>>();
            return response;

        }

        public async Task<BaseResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var cat =await _categoryRepository.FindByIdAsync(id);
                if (cat is null)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "Category not found!"
                    };
                }
                await _categoryRepository.DeleteAsync(cat);
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Category Deleted successfully"
                };
            }
            catch
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "faild"
                };
            }
        }

        public async Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            try { 
                var cat = await _categoryRepository.FindByIdAsync(id);
                if (cat is null)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "Category not found!"
                    };
                }

                if (request.Translations != null) {
                    foreach (var translation in request.Translations) { 
                    var existing=cat.Translations.FirstOrDefault(t=>t.Language==translation.Language);

                        if (existing is not null) { 
                            existing.Name = translation.Name;

                        }
                        else
                        {
                            cat.Translations.Add(new CategoryTranslation
                            {
                                Name = translation.Name,
                                Language = translation.Language,
                            });
                        }
                    }
                }

                await _categoryRepository.UpdateAsync(cat);

                return new BaseResponse()
                {
                    Success = true,
                    Message = "Category Updated Successfully"
                };
            }
            catch
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "faild"
                };
            }

        }
        public async Task<BaseResponse> ToggleStatus(int id)
        {
            try
            {
                var cat = await _categoryRepository.FindByIdAsync(id);
                if (cat is null)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "Category not found!"
                    };
                }
                cat.Status = cat.Status == Status.Active ? Status.InActive : Status.Active;
                await _categoryRepository.UpdateAsync(cat);
                return new BaseResponse()
                {
                    Success = true,
                    Message = "category status apdated successfully"
                };

            }
            catch(Exception ex) 
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "failed",
                    Errors=new List<string> {ex.Message}
                };
            }
        }
    }
}
