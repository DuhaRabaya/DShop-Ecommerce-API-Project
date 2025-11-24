using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using DSHOP.DAL.Repository;
using Mapster;
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
        public CategoryResponse Create(CategoryRequest request)
        {
            var cat = request.Adapt<Category>();
            _categoryRepository.Create(cat);
            return cat.Adapt<CategoryResponse>();
        }

        public List<CategoryResponse> GetAll()
        {
            var cats=_categoryRepository.GetAll();
            var response = cats.Adapt<List<CategoryResponse>>();
            return response;

        }
    }
}
