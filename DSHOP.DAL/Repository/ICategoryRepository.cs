using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category request);
        Task<Category?> FindByIdAsync(int id);
        Task DeleteAsync(Category cat);
        Task<Category?> UpdateAsync(Category cat);
    }
}
