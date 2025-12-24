using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsyncForAdmin();
        Task<List<CategoryUserResponse>> GetAllAsyncForUser([FromQuery] string lang = "en");
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<BaseResponse> DeleteCategoryAsync(int id);
        Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<BaseResponse> ToggleStatus(int id);
    }
}
